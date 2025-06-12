using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace RatesService.API.Controllers;

[ApiController]
[Route("api/rate")]
public class RateController : ControllerBase
{
    private readonly IMediator _mediator;

    public RateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("sync")]
    public async Task<IActionResult> SyncRates()
    {
        await _mediator.Send(new SyncRatesCommand());
        return Ok("Rates processed successfully.");
    }
}