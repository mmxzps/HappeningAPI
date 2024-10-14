using EventVault.Models;
using EventVault.Models.DTOs;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();

        Task<bool> AddEventToDbAsync(Event eventToAdd);
    }
}
