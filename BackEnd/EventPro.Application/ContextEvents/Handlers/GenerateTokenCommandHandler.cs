using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EventPro.Application.ContextEvents.Commands;
using EventPro.Domain.ContextEvent.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace EventPro.Application.ContextEvents.Handlers;

public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, string>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GenerateTokenCommandHandler(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.UserUpdateDto);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.TokenKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user).Result,
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }  
    private async Task<ClaimsIdentity> GenerateClaims(User user)
    {
        var claimsIdent = new ClaimsIdentity();
        claimsIdent.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claimsIdent.AddClaim(new Claim(ClaimTypes.Name, user.UserName ?? string.Empty));
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claimsIdent.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        return claimsIdent;
    }
}