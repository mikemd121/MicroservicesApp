

using MediatR;

namespace RatesService {
    public class RateChangedIntegrationEvent : IDomainEvent, INotification
    {
        public string Symbol { get; set; }
        public decimal NewRate { get; set; }
    }
}
