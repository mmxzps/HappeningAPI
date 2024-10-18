using EventVault.Models;
using EventVault.Models.DTOs;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();

        Task<EventHolder> GetEventInCityAsync(string city);

        Task<bool> AddEventToDbAsync(Event eventToAdd);
    }
}
