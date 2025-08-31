using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.Properties.Application.Features.Properties.Commands.CreateProperty;
using Million.Properties.Application.Features.Properties.Queries.GetPropertyById;

namespace Million.Properties.API.Controllers.V1;

[ApiController]
[Route("api/[controller]")]
public class PropertyController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePropertyCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetPropertyByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

}
