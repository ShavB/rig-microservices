namespace Authentication.Service.Repository;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username);
    Task<User> Register(User user);
    Task<bool> UserExists(string username);
}
