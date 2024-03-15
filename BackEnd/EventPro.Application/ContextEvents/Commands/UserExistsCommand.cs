using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UserExistsCommand : IRequest<bool>
{
    public string UserName { get; set; }
}