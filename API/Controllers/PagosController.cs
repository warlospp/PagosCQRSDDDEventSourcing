using MediatR;
using Microsoft.AspNetCore.Mvc;
using PagosCQRSDDDEventSourcing.Application.Commands;

namespace PagosCQRSDDDEventSourcing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PagosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePagoCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var pago = await _mediator.Send(new GetPagoByIdQuery(id));
        return pago is not null ? Ok(pago) : NotFound();
    }
}
