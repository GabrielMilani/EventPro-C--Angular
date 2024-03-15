using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities.Identity;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Application.ContextEvents.Handlers;

public class CheckUserPasswordCommandHandler : IRequestHandler<CheckUserPasswordCommand, SignInResult>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public CheckUserPasswordCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> Handle(CheckUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(
            user => user.UserName == request.UserUpdateDto.UserName.ToLower());
        return await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    }
}