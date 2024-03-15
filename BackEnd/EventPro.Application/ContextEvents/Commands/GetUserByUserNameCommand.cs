using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GetUserByUserNameCommand : IRequest<UserUpdateDto>
{
    public string UserName { get; set; }
}