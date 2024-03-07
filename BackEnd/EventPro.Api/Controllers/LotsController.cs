using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.ContextEvents.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;
[Route("v1/lots")]
[ApiController]
public class LotsController : ControllerBase
{
    private readonly IMediator _mediator;
    public LotsController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetLot()
    {
        var query = new GetLotsQuery();
        var lots = await _mediator.Send(query);
        return Ok(lots);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLot(int id, UpdateLotCommand command)
    {
        command.Id = id;
        var updatedLot = await _mediator.Send(command);
        return updatedLot != null ? Ok(updatedLot) : NotFound("Lot not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLot(int id)
    {
        var command = new DeleteLotCommand { Id = id };
        var deletedLot = await _mediator.Send(command);
        return deletedLot != null ? Ok(deletedLot) : NotFound("Lot not found");
    } 
}