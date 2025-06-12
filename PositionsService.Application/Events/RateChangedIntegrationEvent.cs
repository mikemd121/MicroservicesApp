using MediatR;

namespace PositionsService
{
   public class RateChangedIntegrationEvent :IDomainEvent, INotification
    {
        public string Symbol { get; set; }
        public decimal NewRate { get; set; }

        public decimal Previous { get; set; }
        public decimal Current { get; set; }
    }
}
