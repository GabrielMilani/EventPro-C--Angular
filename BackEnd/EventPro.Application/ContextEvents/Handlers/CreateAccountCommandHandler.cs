using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Application.DTOs;
using EventPro.Domain.ContextEvent.Entities.Identity;
using EventPro.Domain.ContextShared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventPro.Application.ContextEvents.Handlers;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CreateAccountCommandHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserDto);
        var result = await _userManager.CreateAsync(user, request.UserDto.Password);
        if (result.Succeeded)
        {
            var userToReturn = _mapper.Map<UserDto>(user);
            return userToReturn;
        }
        return null;
    }
}