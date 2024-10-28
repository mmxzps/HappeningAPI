
using EventVault.Models.DTOs;

namespace EventVault.Services.IServices
{
    public interface IVisitStockholmServices
    {
        Task<IEnumerable<EventGetDTO>> GetResponse(string url);
    }
}
