using EventVault.Models.DTOs;
using EventVault.Models.ViewModels;

namespace EventVault.Services.IServices
{
    public interface IVenueServices
    {
        Task AddVenueAsync(VenueCreateDTO venueCreateDTO);
        Task<IEnumerable<VenueGetDTO>> GetAllVenuesAsync();
        Task<VenueGetDTO> GetVenueAsync(int id);
        Task<bool> UpdateVenueAsync(VenueUpdateDTO venueUpdateDTO);
        Task<bool> DeleteVenueAsync(int id);
    }
}
