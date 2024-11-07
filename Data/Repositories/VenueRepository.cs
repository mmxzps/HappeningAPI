using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EventVault.Models;
using EventVault.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Web;

namespace EventVault.Data.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly EventVaultDbContext _context;

        public VenueRepository(EventVaultDbContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<Venue>> GetAllVenuesAsync()
        {
            var venues = await _context.Venues
                .Include(v => v.Events)
                .ToListAsync() ?? new List<Venue>();

            return venues;
        }

        public async Task<Venue> GetVenueAsync (int? id)
        {
            var venue = await _context.Venues.Include(v => v.Events).Where(v => v.Id == id).FirstOrDefaultAsync();

            return venue;
        }

        public async Task<Venue> GetVenueIfInDb(string name, string address)
        {
            var venue = await _context.Venues
                .Where(v => v.Name.ToUpper() == name.ToUpper() && v.Address.ToUpper() == address.ToUpper())
                .FirstOrDefaultAsync();

            return venue;
        }

        //used when creating event to add event to venues.
        public async Task<bool> AddEventToVenue(Event eventToAdd, Venue venue)
        {
            if (venue.Id != null && eventToAdd.Id != null)
            {
                venue.Events.Add(eventToAdd);

                await _context.SaveChangesAsync();

                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task AddVenueAsync(Venue venue)
        {
            await _context.Venues.AddAsync(venue);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateVenueAsync(Venue venue)
        {
            _context.Venues.Update(venue);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteVenueAsync(Venue venue)
        {
            _context.Venues.Remove(venue);

            await _context.SaveChangesAsync();
        }
    }
}
