using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionsService
{

    public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, List<Position>>
    {
        private readonly PositionDbContext _context;

        public GetAllPositionsQueryHandler(PositionDbContext context)
        {
            _context = context;
        }

        public Task<List<Position>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Positions.ToList());
        }
    }

}
