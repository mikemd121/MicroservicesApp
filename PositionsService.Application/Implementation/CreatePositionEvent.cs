

namespace PositionsService
{
   public class CreatePositionEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
    }
}
