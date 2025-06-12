using MediatR;

namespace PositionsService.Application.Commands
{
    public class CreatePositionCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal InitialRate { get; set; }
        public int Side { get; set; } // +1 for BUY, -1 for SELL
    }
}
