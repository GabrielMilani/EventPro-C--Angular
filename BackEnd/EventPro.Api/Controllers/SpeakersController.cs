using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.ContextEvents.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;
[Route("v1/speakers")]
[ApiController]
public class SpeakersController : ControllerBase
{
    private readonly IMediator _mediator;
    public SpeakersController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetSpeaker()
    {
        var query = new GetSpeakersQuery();
        var speaker = await _mediator.Send(query);
        return Ok(speaker);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpeakerById(int id)
    {
        var query = new GetSpeakerByIdQuery { Id = id };
        var speaker = await _mediator.Send(query);
        return speaker != null ? Ok(speaker) : NotFound("Speaker not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpeaker(CreateSpeakerCommand command)
    {
        var createdSpeaker= await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSpeaker), new { id = createdSpeaker.Id }, createdSpeaker);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpeaker(int id, UpdateSpeakerCommand command)
    {
        command.Id = id;
        var updatedSpeaker = await _mediator.Send(command);
        return updatedSpeaker != null ? Ok(updatedSpeaker) : NotFound("Speaker not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpeaker(int id)
    {
        var command = new DeleteSpeakerCommand { Id = id };
        var deletedSpeaker = await _mediator.Send(command);
        return deletedSpeaker != null ? Ok(deletedSpeaker) : NotFound("Speaker not found");
    }       
}