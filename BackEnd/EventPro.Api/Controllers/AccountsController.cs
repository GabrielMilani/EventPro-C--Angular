using System.Security.Claims;
using EventPro.Api.Extensions;
using EventPro.Api.Helpers;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EventPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUtils _utils;
    private readonly string _destination = "profile";

    public AccountsController(IMediator mediator, IUtils utils)
    {
        _mediator = mediator;
        _utils = utils;
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
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var userExists = new UserExistsCommand
        {
          UserName = userDto.UserName
        };
        if (await _mediator.Send(userExists))
            return BadRequest("User already exists!");
        var command = new CreateAccountCommand
        {
            UserDto = userDto
        };
        var user = await _mediator.Send(command);
        if (user != null)
        {
            var tokenCommand = new GenerateTokenCommand()
            {
                UserUpdateDto = user
            };
            var token = await _mediator.Send(tokenCommand);
            return Ok(new
            {
                userName = user.UserName,
                firstName = user.FirstName,
                token = token
            });
        }
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
            firstName = user.Result.FirstName,
            token = token
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
    {
        if (userUpdateDto.UserName != User.GetUserName())
            return Unauthorized("User invalid!");

        var getUserCommand = new GetUserByUserNameCommand()
        {
            UserName = User.GetUserName()
        };
        var user = _mediator.Send(getUserCommand);
        if (user.Result == null) return Unauthorized("UserName invalid!");
        var command = new UpdateAccountCommand
        {
            UserId = User.GetUserId(),
            UserUpdateDto = userUpdateDto
        };
        var returnUser = await _mediator.Send(command);
        var tokenCommand = new GenerateTokenCommand()
        {
            UserUpdateDto = returnUser
        };
        var token = await _mediator.Send(tokenCommand);
        return Ok(new
        {
            userName = returnUser.UserName,
            firstName = returnUser.FirstName,
            token = token
        });
    }
    
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage()
    {
        var query = new GetUserByUserNameCommand
        {
            UserName = User.GetUserName(),
        };
        var userDto = await _mediator.Send(query);
        if (userDto == null) return NoContent();

        var file = Request.Form.Files[0];
        if (file.Length > 0)
        {
            _utils.DeleteImage(userDto.ImageUrl, _destination);
            userDto.ImageUrl = await _utils.SaveImage(file, _destination);
        }
        var command = new UpdateAccountCommand
        {
            UserId = User.GetUserId(),
            UserUpdateDto = userDto
        };
        var userReturn = await _mediator.Send(command);

        return Ok(userReturn);
    }
}