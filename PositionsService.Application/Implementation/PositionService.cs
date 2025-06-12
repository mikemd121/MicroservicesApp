namespace PositionsService
{
  public  class PositionService : IPositionProcessor
    {
        private readonly PositionDbContext _context;
        private readonly IEventDispatcher _dispatcher;

        public PositionService(PositionDbContext context, IEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;

            _dispatcher.Subscribe<RateChangedIntegrationEvent>(OnRateUpdate);
            _dispatcher.Subscribe<CreatePositionEvent>(OnPositionCreate);
        }

        public void OnRateUpdate(RateChangedIntegrationEvent eventItem)
        {
            var positions = _context.Positions.Where(p => p.Symbol == eventItem.Symbol).ToList();
            foreach (var pos in positions)
            {
                pos.Value = pos.Quantity * eventItem.NewRate;
                _dispatcher.Publish(new PositionValuedEvent { Id = pos.Id, Symbol = pos.Symbol, NewValue = pos.Value });
            }
            _context.SaveChanges();
        }

        public void OnPositionCreate(CreatePositionEvent eventItem)
        {
            var pos = new Position { Id = eventItem.Id, Symbol = eventItem.Symbol, Quantity = eventItem.Quantity, Value = 0 };
            _context.Positions.Add(pos);
            _context.SaveChanges();
        }
    }
}
