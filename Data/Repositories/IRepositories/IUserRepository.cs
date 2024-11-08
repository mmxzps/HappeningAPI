using EventVault.Models;
using Microsoft.AspNetCore.Identity;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
