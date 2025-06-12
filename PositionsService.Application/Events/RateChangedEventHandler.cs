using MediatR;

namespace PositionsService
{
    public class RateChangedEventHandler : INotificationHandler<RateChangedIntegrationEvent>
    {
        private readonly PositionDbContext _context;
        private readonly IEventDispatcher _dispatcher;

        public RateChangedEventHandler(PositionDbContext context, IEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }

        public Task Handle(RateChangedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var positions = _context.Positions.Where(p => p.Symbol == notification.Symbol).ToList();
            foreach (var pos in positions)
            {
                pos.Value = pos.Quantity * (notification.NewRate - pos.InitialRate) * pos.Side;
                _dispatcher.Publish(new PositionValuedEvent
                {
                    Id = pos.Id,
                    Symbol = pos.Symbol,
                    NewValue = pos.Value
                });
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
