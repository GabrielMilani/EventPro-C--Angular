using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.ContextEvents.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;
[Route("v1/social")]
[ApiController]
public class SocialNetworksController : ControllerBase
{
    private readonly IMediator _mediator;
    public SocialNetworksController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetSocialNetwork()
    {
        var query = new GetSocialNetworksQuery();
        var socialNetworks = await _mediator.Send(query);
        return Ok(socialNetworks);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSocialNetworkById(int id)
    {
        var query = new GetSocialNetworkByIdQuery{ Id = id };
        var socialNetwork = await _mediator.Send(query);
        return socialNetwork != null ? Ok(socialNetwork) : NotFound("SocialNetwork not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateSocialNetwork(CreateSocialNetworkCommand command)
    {
        var createdSocialNetwork = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSocialNetwork), new { id = createdSocialNetwork.Id }, createdSocialNetwork);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSocialNetwork(int id, UpdateSocialNetworkCommand command)
    {
        command.Id = id;
        var updatedSocialNetwork = await _mediator.Send(command);
        return updatedSocialNetwork != null ? Ok(updatedSocialNetwork) : NotFound("SocialNetwork not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSocialNetwork(int id)
    {
        var command = new DeleteSocialNetworkCommand { Id = id };
        var deletedSocialNetwork = await _mediator.Send(command);
        return deletedSocialNetwork != null ? Ok(deletedSocialNetwork) : NotFound("SocialNetwork not found");
    }    
}