using EventPro.Api.Extensions;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;
[Route("v1/speakers")]
[ApiController]
[Authorize]
public class SpeakersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpeakersController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
    {
        _mediator = mediator;
        _webHostEnvironment = webHostEnvironment;
    }

    private readonly IWebHostEnvironment _webHostEnvironment;

    [HttpGet("all")]
    public async Task<IActionResult> GetSpeakers([FromQuery]PageParams pageParams)
    {
        var command = new GetSpeakersCommand
        {
            PageParams = pageParams,
            IncludeEvents = false
        };
        var events = await _mediator.Send(command);

        Response.AddPagination(events.CurrentPage, events.PageSize, events.TotalCount, events.TotalPages);

        return Ok(events);
    }
    [HttpGet]
    public async Task<IActionResult> GetSpeakerByUserId()
    {
        var command = new GetSpeakerByUserIdCommand
        {
            UserId = User.GetUserId(),
            IncludeEvents = false
        };
        var @event = await _mediator.Send(command);
        return @event != null ? Ok(@event) : NotFound("Speaker not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpeaker(SpeakerAddDto speakerAddDto)
    {
        var commandExistsSpeaker = new GetSpeakerByUserIdCommand
        {
            UserId = User.GetUserId(),
            IncludeEvents = false
        };
        var existsSpeaker = await _mediator.Send(commandExistsSpeaker);
        if (existsSpeaker == null)
        {
            var command = new CreateSpeakerCommand
            {
                UserId = User.GetUserId(),
                SpeakerAddDto = speakerAddDto
            };
            var createdSpeaker = await _mediator.Send(command);
            return createdSpeaker != null ? Ok(createdSpeaker) : NotFound("Speaker created not found");
        }
        else 
            return existsSpeaker != null ? Ok(existsSpeaker) : NotFound("Speaker created not found");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpeaker(SpeakerUpdateDto speakerUpdateDto)
    {
        var command = new UpdateSpeakerCommand
        {
            UserId = User.GetUserId(),
            SpeakerUpdateDto = speakerUpdateDto
        };
        var updatedSpeaker = await _mediator.Send(command);
        return updatedSpeaker != null ? Ok(updatedSpeaker) : NotFound("Speaker updated not found");
    }
}