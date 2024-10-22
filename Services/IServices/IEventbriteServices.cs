using EventVault.Models.Eventbrite;
using EventVault.Models;

namespace EventVault.Services.IServices
{
    public interface IEventbriteServices
    {
        Task<PaginatedResponse<Event>> GetAllEventsAsync(int page = 1);
        Task<Event> GetEventByIdAsync(string eventId);
    }
}
