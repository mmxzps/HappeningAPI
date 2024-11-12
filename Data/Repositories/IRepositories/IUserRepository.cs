using EventVault.Models;
using Microsoft.AspNetCore.Identity;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetOneUserByUserNameAsync(string userName);
        Task<User> GetOneUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
    }
}
