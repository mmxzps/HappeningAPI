using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
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
        public async Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync()
        {
           var allusers =  await _userRepository.GetAllUsersAsync();

            return allusers.Select(u=> new UserGetAllDTO 
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                NickName = u.NickName,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                ProfilePictureUrl = u.ProfilePictureUrl,
            });
        }
    }
}
