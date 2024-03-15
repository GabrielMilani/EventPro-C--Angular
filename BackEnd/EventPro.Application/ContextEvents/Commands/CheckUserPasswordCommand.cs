using EventPro.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPro.Application.ContextEvents.Commands;

public class CheckUserPasswordCommand : IRequest<SignInResult>
{
    public UserUpdateDto UserUpdateDto { get; set; }
    public string Password { get; set; }
}