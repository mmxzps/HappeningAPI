using EventVault.Models;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetAllVenuesAsync();
        Task<Venue> GetVenueAsync(int? id);
        Task<Venue> GetVenueIfInDb(string name, string address, string city);  
        Task AddVenueAsync(Venue venue);
        Task<bool> AddEventToVenue(Event eventToAdd, Venue venue);
        Task UpdateVenueAsync(Venue venue);
        Task DeleteVenueAsync(Venue venue);
    }
}
