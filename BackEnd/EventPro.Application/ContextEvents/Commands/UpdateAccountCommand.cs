﻿using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class UpdateAccountCommand : IRequest<UserUpdateDto>
{
    public int UserId { get; set; }
    public UserUpdateDto UserUpdateDto { get; set; }
}