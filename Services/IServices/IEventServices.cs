using EventVault.Models.DTOs;
using EventVault.Models;
using TicketmasterTesting.Models.TicketMasterModels;
using EventVault.Models.ViewModels;

namespace EventVault.Services.IServices
{
    public interface IEventServices
    {
        Task<bool> AddEventAsync(EventCreateDTO eventCreateDTO);
        Task<IEnumerable<EventGetDTO>> GetAllEventsAsync();
        Task<EventGetDTO> GetEventAsync(int id);
        Task<bool> UpdateEventAsync(EventUpdateDTO eventUpdateDTO);
        Task<bool> DeleteEventAsync(int id);

    }
}
