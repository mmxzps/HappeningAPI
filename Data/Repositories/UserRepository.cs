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

        public async Task<User> GetOneUserByIdAsync(string userId)
        {
            var fetchedUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (fetchedUser == null)
            {
                throw new KeyNotFoundException($"User with id:{userId} wasnt found!");
            }
            return fetchedUser;
        }

        public async Task<User> GetOneUserByUserNameAsync(string userName)
        {
            var fetchedUser = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            if (fetchedUser == null)
            {
                throw new KeyNotFoundException($"User with id:{userName} wasnt found!");
            }
            return fetchedUser;
        }

        public async Task UpdateUserAsync(User user)
        {
            //var fetchedUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            //if (fetchedUser == null)
            //{
            //    throw new KeyNotFoundException($"User with id:{userId} wasnt found!");

            //}
            //fetchedUser.FirstName = user.FirstName;
            //fetchedUser.LastName = user.LastName;
            //fetchedUser.Email = user.Email;
            //fetchedUser.PhoneNumber = user.PhoneNumber;
            //fetchedUser.ProfilePictureUrl = user.ProfilePictureUrl;
            //fetchedUser.NickName = user.NickName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new DbUpdateException($"Couldn't update! Error: {errors}");
            }
        }
    }
}
