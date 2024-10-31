using EventVault.Models;

namespace EventVault.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
