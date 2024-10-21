using EventVault.Models.ViewModels;

namespace EventVault.Services.IServices
{
    public interface IKBEventServices
    {
        Task<KBEventViewModel> GetEventDataAsync(int eventId);

        Task<IEnumerable<KBEventListViewModel>> GetListOfEventsAsync();
    }
}
