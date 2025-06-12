using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionsService.Application.Commands
{
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Unit>
    {
        private readonly PositionDbContext _context;
        private readonly IEventDispatcher _dispatcher;

        public CreatePositionCommandHandler(PositionDbContext context, IEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }

        public Task<Unit> Handle(CreatePositionCommand command, CancellationToken cancellationToken)
        {
            var pos = new Position
            {
                Id = command.Id,
                Symbol = command.Symbol,
                Quantity = command.Quantity,
                InitialRate = command.InitialRate,
                Side = command.Side,
                Value = 0
            };
            _context.Positions.Add(pos);
            _context.SaveChanges();
            return Task.FromResult(Unit.Value);
        }
    }
}
