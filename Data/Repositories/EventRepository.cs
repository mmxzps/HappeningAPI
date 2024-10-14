using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using Microsoft.EntityFrameworkCore;

namespace EventVault.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventVaultDbContext _context;

        public EventRepository(EventVaultDbContext context) {

            _context = context;
        
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync ()
        {
            var eventList = await _context.Events.ToListAsync() ?? new List<Event>();

            return eventList;
        }

        public async Task<bool> AddEventToDbAsync(Event eventToAdd)
        {
            try
            {
                await _context.Events.AddAsync(eventToAdd);

                await _context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
