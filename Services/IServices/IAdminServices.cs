using Microsoft.AspNetCore.Identity;

namespace EventVault.Services.IServices
{
    public interface IAdminServices
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
        Task<IEnumerable<IdentityUser>> SearchUsersAsync(string searchTerm);
    }
}
