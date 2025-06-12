using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionsService
{
    public class GetPositionBySymbolQueryHandler : IRequestHandler<GetPositionBySymbolQuery, List<Position>>
    {
        private readonly PositionDbContext _context;

        public GetPositionBySymbolQueryHandler(PositionDbContext context)
        {
            _context = context;
        }

        public Task<List<Position>> Handle(GetPositionBySymbolQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Positions
                .Where(p => p.Symbol.Equals(request.Symbol, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(result);
        }
    }

}
