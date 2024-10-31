using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Services.IServices;

namespace EventVault.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
           var allusers =  await _userRepository.GetAllUsersAsync();

            return allusers;
        }
    }
}
