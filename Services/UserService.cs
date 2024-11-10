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

        public async Task<UserShowOneDTO> GetOneUserByIdAsync(string userId)
        {
            var userWithId = await _userRepository.GetOneUserByIdAsync(userId);

            var userDTO = new UserShowOneDTO
            {
                FirstName = userWithId.FirstName,
                LastName = userWithId.LastName,
                Email = userWithId.Email,
                PhoneNumber = userWithId.PhoneNumber,
                NickName = userWithId.NickName,
                UserName = userWithId.UserName,
                ProfilePictureUrl = userWithId.ProfilePictureUrl,
            };
            return userDTO;
        }

        public async Task<UserShowOneDTO> GetOneUserByUserNameAsync(string userName)
        {
            var userWithUserName = await _userRepository.GetOneUserByUserNameAsync(userName);

            var userDTO = new UserShowOneDTO
            {
                FirstName = userWithUserName.FirstName,
                LastName = userWithUserName.LastName,
                Email = userWithUserName.Email,
                PhoneNumber = userWithUserName.PhoneNumber,
                NickName = userWithUserName.NickName,
                UserName = userWithUserName.UserName,
                ProfilePictureUrl = userWithUserName.ProfilePictureUrl,
            };
            return userDTO;
        }

        public async Task UpdateUserAsync(string userId, UserUpdateDTO user)
        {
           var getUser = await _userRepository.GetOneUserByIdAsync(userId);

            getUser.FirstName = user.FirstName;
            getUser.LastName = user.LastName;
            getUser.Email = user.Email;
            getUser.PhoneNumber = user.PhoneNumber;
            getUser.NickName = user.NickName;
            getUser.ProfilePictureUrl = user.ProfilePictureUrl;

            await _userRepository.UpdateUserAsync(getUser);
        }
    }
}
