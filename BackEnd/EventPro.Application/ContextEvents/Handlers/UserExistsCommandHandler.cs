using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class UserExistsCommandHandler : IRequestHandler<UserExistsCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserExistsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UserExistsCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.AnyUser(request.UserName);
    }
}