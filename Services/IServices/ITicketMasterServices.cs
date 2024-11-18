using EventVault.Models.ViewModels;

namespace EventVault.Services.IServices
{
    public interface ITicketMasterServices
    {
        Task<IEnumerable<EventViewModel>> GetEventsInCityAsync(string city);
    }
}