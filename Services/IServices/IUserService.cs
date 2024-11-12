using EventVault.Models;
using EventVault.Models.DTOs;

namespace EventVault.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync();
        Task<UserShowOneDTO> GetOneUserByUserNameAsync(string userName);
        Task<UserShowOneDTO> GetOneUserByIdAsync(string userId);
        Task UpdateUserAsync(string userId, UserUpdateDTO user); //nu ska vi översätta user till DTO i services o sedan göra endpoint
    }
}
