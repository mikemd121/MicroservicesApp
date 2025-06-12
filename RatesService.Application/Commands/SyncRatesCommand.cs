using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesService
{
    public class SyncRatesCommand : IRequest<Unit> { }

}
