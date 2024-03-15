using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;

namespace EventPro.Application.ContextEvents.Handlers;

public class GetUserByUserNameCommandHandler : IRequestHandler<GetUserByUserNameCommand, UserUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByUserNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserUpdateDto> Handle(GetUserByUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(request.UserName);
        if (user == null) return null;

        return _mapper.Map<UserUpdateDto>(user);
    }
}