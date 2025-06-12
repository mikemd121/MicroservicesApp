namespace PositionsService
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal InitialRate { get; set; }  // Added for P/L calculation
        public int Side { get; set; }              // +1 for BUY, -1 for SELL
        public decimal Value { get; set; }
    }
}
