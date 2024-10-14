using EventVault.Models.DTOs;
using EventVault.Models;

namespace EventVault.Services.IServices
{
    public interface IEventServices
    {
        Task<IEnumerable<EventGetDTO>> GetAllEventsAsync();

        Task<bool> AddEventToDbAsync(EventCreateDTO eventCreateDTO);
    }
}
