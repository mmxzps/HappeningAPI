using EventVault.Models;
using EventVault.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetOneUserByUserNameAsync(string userName);
        Task<User> GetOneUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
        Task AddEventToUserAsync(string userId, EventCreateDTO eventCreateDTO);
        Task<User> GetUserAsync(string userId);
        Task<Event> GetSavedEventAsync(string userId, int eventId);
        Task<IEnumerable<Event>> GetAllSavedEventsAsync(string userId);
        Task RemoveEventFromUserAsync(string userId, int eventId);
    }
}
