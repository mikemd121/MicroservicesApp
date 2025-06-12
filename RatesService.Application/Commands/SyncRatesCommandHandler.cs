using MediatR;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace RatesService
{
    public class SyncRatesCommandHandler : IRequestHandler<SyncRatesCommand, Unit>
    {
        private readonly RateDbContext _context;
        private readonly IEventDispatcher _dispatcher;
        private static readonly HttpClient _httpClient = new();

        public SyncRatesCommandHandler(RateDbContext context, IEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "7e171d6e-7fb5-4713-997e-d8e976f6abb2");
        }

        public async Task<Unit> Handle(SyncRatesCommand request, CancellationToken cancellationToken)
        {
            var baseUrl = "https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";

            // Build query manually
            var query = $"start=1&limit=5000&convert=USD";
            var fullUrl = $"{baseUrl}?{query}";

            var requestHttp = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            requestHttp.Headers.Add("X-CMC_PRO_API_KEY", "7e171d6e-7fb5-4713-997e-d8e976f6abb2");
            requestHttp.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(requestHttp);
            response.EnsureSuccessStatusCode();
            var result= await response.Content.ReadAsStringAsync();

            var root = JObject.Parse(result);
            var rates = root["data"]
                .Select(token => new Rate
                {
                    Symbol = (string)token["symbol"],
                    Price = (decimal)token["quote"]["USD"]["price"]
                })
                .ToList();

            foreach (var rate in rates)
            {
                var existing = await _context.Rates.FindAsync(rate.Symbol);
                if (existing != null)
                {
                    var change = Math.Abs(rate.Price - existing.Price) / existing.Price;
                    if (change > 0.05m)
                    {
                        _dispatcher.Publish(new RateChangedIntegrationEvent
                        {
                            Symbol = rate.Symbol,
                            NewRate = rate.Price
                        });
                    }
                    existing.Price = rate.Price;
                }
                else
                {
                    _context.Rates.Add(rate);
                }
            }
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }

}