using EventPro.Api.Extensions;
using EventPro.Application.ContextEvents.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EventPro.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EventPro.Api.Controllers;
[Route("v1/socialnetworks")]
[ApiController]
[Authorize]
public class SocialNetworksController : ControllerBase
{
    private readonly IMediator _mediator;

    public SocialNetworksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetAllByEventId(int eventId)
    {
        if (!(await AuthorEvent(eventId)))
            return Unauthorized();
        var command = new GetSocialNetworksEventCommand
        {
            EventId = eventId
        };
        var lots = await _mediator.Send(command);
        return Ok(lots);
    }

    [HttpGet("speaker")]
    public async Task<IActionResult> GetAllBySpeakerId()
    {
        var commandSpeaker = new GetSpeakerByUserIdCommand()
        {
            UserId = User.GetUserId(),
            IncludeEvents = false
        };
        var speakers = await _mediator.Send(commandSpeaker);
        if (speakers == null)
            return Unauthorized();

        var commandSocialNetwork = new GetSocialNetworksSpeakerCommand
        {
            SpeakerId = User.GetUserId()
        };
        var socialNewtork = await _mediator.Send(commandSocialNetwork);
        if (socialNewtork == null)
            return NoContent();

        return Ok(socialNewtork);
    }

    [HttpPut("event/{eventId}")]
    public async Task<IActionResult> SaveSocialNetworkByEvent(int eventId, SocialNetworkDto[] socialNetworkDto)
    {
        if (!(await AuthorEvent(eventId)))
            return Unauthorized();

        var command = new SaveSocialNetworkByEventCommand
        {
            EventId = eventId,
            SocialNetworkDto = socialNetworkDto
        };
        var updatedSocialnetwork = await _mediator.Send(command);
        return updatedSocialnetwork != null ? Ok(updatedSocialnetwork) : NotFound("Updated Social Network by Event not found");
    }

    [HttpPut("speaker")]
    public async Task<IActionResult> SaveSocialNetworkBySpeaker( SocialNetworkDto[] socialNetworkDto)
    {
        var commandSpeaker = new GetSpeakerByUserIdCommand()
        {
            UserId = User.GetUserId(),
            IncludeEvents = false
        };
        var speakers = await _mediator.Send(commandSpeaker);
        if (speakers == null)
            return Unauthorized();

        var commandSocialNetwork = new SaveSocialNetworkBySpeakerCommand
        {
            SpeakerId = speakers.Id,
            SocialNetworkDto = socialNetworkDto
        };
        var updatedSocialNetwork = await _mediator.Send(commandSocialNetwork);
        return updatedSocialNetwork != null ? Ok(updatedSocialNetwork) : NotFound("Updated Social Network by Speaker not found");
    }


    [HttpDelete("event/{eventId}/{id}")]
    public async Task<IActionResult> DeleteByEvent(int eventId, int id)
    {                                                                                             
        if (!(await AuthorEvent(eventId)))
            return Unauthorized();

        var command = new DeleteSocialNetworkEventCommand
        {
            EventId = eventId,                           
            SocialNetworkId = id
        };
        var deletedSocialNetwork = await _mediator.Send(command);
        return deletedSocialNetwork != null ? Ok(deletedSocialNetwork) : NotFound("Deleted Social Network not found");
    }

    [HttpDelete("speaker/{id}")]
    public async Task<IActionResult> DeleteBySpeaker(int id)
    {
        var commandSpeaker = new GetSpeakerByUserIdCommand()
        {
            UserId = User.GetUserId(),
            IncludeEvents = false
        };
        var speakers = await _mediator.Send(commandSpeaker);
        if (speakers == null)
            return Unauthorized();
        var command = new DeleteSocialNetworkSpeakerCommand
        {
            SpeakerId = speakers.Id,                           
            SocialNetworkId = id
        };
        var deletedSocialNetwork = await _mediator.Send(command);
        return deletedSocialNetwork != null ? Ok(deletedSocialNetwork) : NotFound("Deleted Social Network not found");
    }

    [NonAction]
    private async Task<bool> AuthorEvent(int eventId)
    {
        var commandEvent = new GetEventByIdCommand
        {
            UserId = User.GetUserId(),
            EventId = eventId
        };
       var @event = await _mediator.Send(commandEvent);
       if (@event == null) 
           return false;
       
       return true;
    }
}