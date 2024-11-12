using EventVault.Models;
using EventVault.Models.DTOs;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IEventRepository
    {
        Task AddEventAsync(Event eventToAdd);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventAsync(int? id);
        Task UpdateEventAsync(Event eventToUpdate);
        Task DeleteEventAsync(Event eventToDelete);
        Task<Event?> GetEventFromDb(EventCreateDTO PossibleEventInDb);
    }
}
