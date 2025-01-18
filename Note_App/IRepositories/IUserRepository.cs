using Note_App.Models;

namespace Note_App.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> RegisterUserAsync(User user);
    }
}
