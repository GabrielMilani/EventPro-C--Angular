﻿using EventPro.Application.ContextEvents.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EventPro.Application.DTOs;

namespace EventPro.Api.Controllers;
[Route("v1/lots")]
[ApiController]
public class LotsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetLots(int eventId)
    {
        var query = new GetLotsByEventIdCommand()
        {
            EventId = eventId
        };
        var lots = await _mediator.Send(query);
        return Ok(lots);
    }
    [HttpGet("{eventId}/{id}")]
    public async Task<IActionResult> GetLotByIds(int eventId, int id)
    {
        var query = new GetLotByIdsCommand()
        {
            EventId = eventId,
            Id = id
        };
        var lots = await _mediator.Send(query);
        return Ok(lots);
    }

    [HttpPut("{eventId}")]
    public async Task<IActionResult> SaveLots(int eventId, LotDto[] lots)
    {
        var command = new SaveLotsCommand();
        command.EventId = eventId;
        command.Lots = lots;

        var updatedLot = await _mediator.Send(command);
        return updatedLot != null ? Ok(updatedLot) : NotFound("Lot not found");
    }

    [HttpDelete("{eventId}/{id}")]
    public async Task<IActionResult> DeleteLot(int eventId, int id)
    {
        var command = new DeleteLotByIdsCommand
        {
            EventId = eventId,
            Id = id
        };
        var deletedLot = await _mediator.Send(command);
        return deletedLot != null ? Ok(deletedLot) : NotFound("Lot not found");
    } 
}