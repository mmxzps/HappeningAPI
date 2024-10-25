
namespace EventVault.Services.IServices
{
    public interface IVisitStockholmServices
    {
        Task<IEnumerable<Event>> GetResponse(string url);
    }
}
