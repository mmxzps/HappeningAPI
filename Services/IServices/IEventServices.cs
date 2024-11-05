using EventVault.Models.DTOs;
using EventVault.Models;
using TicketmasterTesting.Models.TicketMasterModels;
using EventVault.Models.ViewModels;

namespace EventVault.Services.IServices
{
    public interface IEventServices
    {

        Task<IEnumerable<EventGetDTO>> GetAllEventsAsync();
        Task<EventGetDTO> GetEventById(int id);
        Task<bool> AddEventToDbAsync(EventCreateDTO eventCreateDTO);

    }
}
