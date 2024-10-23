using EventVault.Models.ViewModels;

namespace EventVault.Services.IServices
{
    public interface IKBEventServices
    {
        Task<List<KBEventViewModel>> GetEventDataAsync();

        Task<IEnumerable<KBEventListViewModel>> GetListOfEventsAsync();
    }
}
