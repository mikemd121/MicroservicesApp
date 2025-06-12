
using Microsoft.AspNetCore.Mvc;

namespace PositionsService
{
    [ApiController]
    [Route("api/position")]
    public class PositionController : ControllerBase
    {
        private readonly IEventDispatcher _dispatcher;

        public PositionController(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreatePositionEvent evt)
        {
            _dispatcher.Publish(evt);
            return Ok("Position creation triggered.");
        }
    }

}
