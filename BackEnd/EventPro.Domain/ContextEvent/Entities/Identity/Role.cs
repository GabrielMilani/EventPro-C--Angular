using Microsoft.AspNetCore.Identity;

namespace EventPro.Domain.ContextEvent.Entities.Identity;

public class Role : IdentityRole<int>
{
    public List<UserRole> UserRoles { get; set; }
}