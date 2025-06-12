namespace PositionsService
{
    public class PositionValuedEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal NewValue { get; set; }
    }
}
