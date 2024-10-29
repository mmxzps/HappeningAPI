using EventVault.Data;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventVault.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly EventVaultDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminServices(EventVaultDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<IdentityUser>> SearchUsersAsync(string searchTerm)
        {
            return await _userManager.Users
                .Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
