using EventPro.Application.DTOs;
using MediatR;

namespace EventPro.Application.ContextEvents.Commands;

public class GenerateTokenCommand : IRequest<string>
{
    public UserUpdateDto? UserUpdateDto { get; set; }
}