using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventVault.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return users;
        }
    }
}
