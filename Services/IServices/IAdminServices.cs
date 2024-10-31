using EventVault.Models;
using Microsoft.AspNetCore.Identity;

namespace EventVault.Services.IServices
{
    public interface IAdminServices
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);
    }
}
