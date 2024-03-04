using EventPro.Application.EventContext.Event.Commands;
using EventPro.Application.EventContext.Event.Queries;
using EventPro.Application.EventContext.Lot.Commands;
using EventPro.Application.EventContext.Lot.Queries;
using EventPro.Domain.EventContext.Entities;
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLotById(int id)
    {
        var query = new GetLotByIdQuery { Id = id };
        var lot = await _mediator.Send(query);
        return lot != null ? Ok(lot) : NotFound("Lot not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateLot(CreateLotCommand command)
    {
        var createdLot = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLot), new { id = createdLot.Id }, createdLot);
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