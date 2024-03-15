using System.Security.Claims;
using EventPro.Api.Extensions;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        var getUserCommand = new GetUserByUserNameCommand()
        {
            UserName = User.GetUserName()
        };
        var user = await _mediator.Send(getUserCommand);
        return Ok(user);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CreateAccountCommand command)
    {
        var userExists = new UserExistsCommand();
        userExists.UserName = command.UserDto.UserName;
        if (await _mediator.Send(userExists))
            return BadRequest("User already exists!");
        var user = await _mediator.Send(command);
        if (user != null)
            return Ok(user);
        return BadRequest("Unregistered user!");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLogin)
    {
        var getUserCommand = new GetUserByUserNameCommand()
        {
            UserName = userLogin.UserName
        };
        var user = _mediator.Send(getUserCommand);
        if (user.Result == null) return Unauthorized("UserName or Password invalid!");
        var checkUserPassword = new CheckUserPasswordCommand()
        {
            Password = userLogin.Password,
            UserUpdateDto = user.Result
        };
        var result = await _mediator.Send(checkUserPassword);
        if (!result.Succeeded) return Unauthorized();

        var tokenCommand = new GenerateTokenCommand()
        {
            UserUpdateDto = user.Result
        };
        var token = await _mediator.Send(tokenCommand);
        return Ok(new
        {
            userName = user.Result.UserName,
            FirstName = user.Result.FirstName,
            token = token
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateAccountCommand command)
    {
        var getUserCommand = new GetUserByUserNameCommand()
        {
            UserName = User.GetUserName()
        };
        var user = _mediator.Send(getUserCommand);
        if (user.Result == null) return Unauthorized("UserName invalid!");
        var returnUser = await _mediator.Send(command);
        return Ok(returnUser);
    }
}