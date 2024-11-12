using EventVault.Models;
using EventVault.Models.DTOs;
using System.ComponentModel;

namespace EventVault.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync();
        Task<IEnumerable<EventGetDTO>> GetAllSavedEventsAsync (string userId);
        Task<EventGetDTO> GetSavedEventAsync (string userId, int eventId);
        Task AddEventToUserAsync (string userId, EventCreateDTO eventCreateDTO);
        Task RemoveEventFromUserAsync (string userId, int eventId);

    }
}
