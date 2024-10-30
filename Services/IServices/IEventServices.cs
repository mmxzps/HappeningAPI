using EventVault.Models.DTOs;
using EventVault.Models;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Services.IServices
{
    public interface IEventServices
    {
        Task<IEnumerable<EventGetDTO>> GetAllEventsAsync();
        Task<List<ShowEventDTO>> GetEventInCityAsync(string city);
        Task<bool> AddEventToDbAsync(EventCreateDTO eventCreateDTO);
    }
}
