using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionsService
{
    public class GetAllPositionsQuery : IRequest<List<Position>> { }
}
