using EventPro.Domain.ContextEvent.Entities.Enum;
using Microsoft.AspNetCore.Identity;

namespace EventPro.Domain.ContextEvent.Entities.Identity;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ETitle? Title { get; set; }
    public string? Description { get; set; }
    public EFunction? Function { get; set; }
    public string? ImageUrl { get; set; }
    public List<UserRole>? UserRoles { get; set; }
}