using EventPro.Domain.ContextEvent.Abstractions;
using EventPro.Domain.ContextEvent.Entities.Identity;
using EventPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPro.Infrastructure.Context.ContextEvent.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUser(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));
        await _context.Users.AddAsync(user);
        return user;
    }

    public async Task<User> DeleteUser(int userId)
    {
        var user = await GetUserById(userId);
        if (user is null)
            throw new InvalidOperationException("User not found");
        _context.Users.Remove(user);
        return user;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var userList = await _context.Users.ToListAsync();
        return userList ?? Enumerable.Empty<User>();
    }

    public async Task<User> GetUserById(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
            throw new InvalidOperationException("User not found");
        return user;
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName.ToLower());
        if (user is null)
            throw new InvalidOperationException("User not found");
        return user;
    }

    public void UpdateUser(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));
        _context.Users.Update(user);
    }

    public async Task<bool> AnyUser(string userName)
    {
        return await _context.Users.AnyAsync(user => user.UserName == userName.ToLower());
    }
}