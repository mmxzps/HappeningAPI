using EventVault.Models;
using EventVault.Models.DTOs;

namespace EventVault.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync();
    }
}
