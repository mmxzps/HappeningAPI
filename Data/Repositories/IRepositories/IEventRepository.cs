using EventVault.Models;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync(int userId);
    }
}
