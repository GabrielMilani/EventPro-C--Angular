using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities.Identity;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPro.Application.ContextEvents.Handlers;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, UserUpdateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UpdateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserUpdateDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserName(request.UserUpdateDto.UserName);
        if (user == null) return null;

        _mapper.Map(request.UserUpdateDto, user);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.UserUpdateDto.Password);

        _unitOfWork.UserRepository.UpdateUser(user);
        if (_unitOfWork.CommitAsync().IsCompletedSuccessfully)
        {
            var userReturn = await _unitOfWork.UserRepository.GetUserByUserName(user.UserName);
            return _mapper.Map<UserUpdateDto>(userReturn);
        }
        return null;
    }
}