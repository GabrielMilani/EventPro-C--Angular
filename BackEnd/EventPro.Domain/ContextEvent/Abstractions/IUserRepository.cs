using EventPro.Domain.ContextEvent.Entities.Identity;

namespace EventPro.Domain.ContextEvent.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserById(int userId);
    Task<User> GetUserByUserName(string userName);
    Task<bool> AnyUser(string userName);
    Task<User> AddUser(User user);
    void UpdateUser(User user);
    Task<User> DeleteUser(int userId); 
}