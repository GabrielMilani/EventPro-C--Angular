using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class CreateAccountCommand : IRequest<UserDto>
{
    public UserDto UserDto { get; set; }
}