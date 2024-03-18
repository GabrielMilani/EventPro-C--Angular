using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class CreateAccountCommand : IRequest<UserUpdateDto>
{
    public UserDto UserDto { get; set; }
}