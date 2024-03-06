using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.ContextEvents.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;
[Route("v1/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    public EventsController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetEvent()
    {
        var query = new GetEventsQuery();
        var events = await _mediator.Send(query);
        return Ok(events);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var query = new GetEventByIdQuery { Id = id };
        var @event = await _mediator.Send(query);
        return @event != null ? Ok(@event) : NotFound("Event not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventCommand command)
    {
        var createdEvent = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, UpdateEventCommand command)
    {
        command.Id = id;
        var updatedEvent = await _mediator.Send(command);
        return updatedEvent != null ? Ok(updatedEvent) : NotFound("Event not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var command = new DeleteEventCommand { Id = id };
        var deletedEvent = await _mediator.Send(command);
        return deletedEvent != null ? Ok(deletedEvent) : NotFound("Event not found");
    } 
}