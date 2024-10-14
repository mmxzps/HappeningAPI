using EventVault.Models.DTOs;

namespace EventVault.Services.IServices
{
    public interface IEventServices
    {
        Task<IEnumerable<EventGetDTO>> GetEventsAsync (int userId);
    }
}
