using EventPro.Api.Extensions;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextShared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;
[Route("v1/events")]
[ApiController]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
    {
        _mediator = mediator;
        _webHostEnvironment = webHostEnvironment;
    }

    private readonly IWebHostEnvironment _webHostEnvironment;

    [HttpGet]
    public async Task<IActionResult> GetEvent([FromQuery]PageParams pageParams)
    {
        var command = new GetEventsCommand
        {
            UserId = User.GetUserId(),
            PageParams = pageParams,
            IncludeSpeaker = false
        };
        var events = await _mediator.Send(command);

        Response.AddPagination(events.CurrentPage, events.PageSize, events.TotalCount, events.TotalPages);

        return Ok(events);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var command = new GetEventByIdCommand
        {
            UserId = User.GetUserId(),
            EventId = id
        };
        var @event = await _mediator.Send(command);
        return @event != null ? Ok(@event) : NotFound("Event not found");
    }
    [HttpPost("upload-image/{eventId}")]
    public async Task<IActionResult> UploadImage(int eventId)
    {
        var query = new GetEventByIdCommand
        {
            UserId = User.GetUserId(),
            EventId = eventId
        };
        var eventDto = await _mediator.Send(query);
        if (eventDto == null) return NoContent();

        var file = Request.Form.Files[0];
        if (file.Length > 0)
        {
           DeleteImage(eventDto.ImageUrl);
           eventDto.ImageUrl = await SaveImage(file);
        }
        var command = new UploadImageEventCommand
        {
            Id = eventId,
            EventDto = eventDto
        };
        command.Id = eventId;
        command.EventDto = eventDto;
        var eventReturn = await _mediator.Send(command);

        return Ok(eventDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventCommand command)
    {
        command.UserId = User.GetUserId();
        var createdEvent = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, UpdateEventCommand command)
    {
        command.Id = id;
        command.UserId = User.GetUserId();
        var updatedEvent = await _mediator.Send(command);
        return updatedEvent != null ? Ok(updatedEvent) : NotFound("Event not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var command = new DeleteEventCommand
        {
            UserId = User.GetUserId(),
            Id = id
        };
        var deletedEvent = await _mediator.Send(command);
        if (deletedEvent != null)
        {
           DeleteImage(deletedEvent.ImageUrl);
           return Ok(deletedEvent);

        }
        else 
            return NotFound("Event not found");
    }

    [NonAction]
    public async Task<string> SaveImage(IFormFile imageFile)
    {
        string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                        .Take(10).ToArray()).Replace(' ','-');
        imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
        var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources/images", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        } ;
        return imageName;
    }

    [NonAction]
    public void DeleteImage(string imageName)
    {
        var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources/images", imageName);
        if (System.IO.File.Exists(imagePath))
            System.IO.File.Delete(imagePath);
    }
}