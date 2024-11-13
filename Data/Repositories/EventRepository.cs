using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventVaultDbContext _context;

        public EventRepository(EventVaultDbContext context)
        {
            _context = context;
        }

        //create
        public async Task AddEventAsync(Event eventToAdd)
        {
            await _context.Events.AddAsync(eventToAdd);

            await _context.SaveChangesAsync();
        }

        //get all events
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Venue)
                .ToListAsync();
        }

        //get event by ID
        public async Task<Event> GetEventAsync(int? id)
        {
            var eventById = await _context.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            return eventById;
        }


        //update event by Id
        public async Task UpdateEventAsync(Event eventUpdateDTO)
        {
            _context.Events.Update(eventUpdateDTO);

            await _context.SaveChangesAsync();
        }

        //delete event
        public async Task DeleteEventAsync(Event eventToDelete)
        {
            //find venue of event
            var venue = await _context.Venues
                .Include(v => v.Events)
                .FirstOrDefaultAsync(v => v.Id == eventToDelete.FK_Venue);


            //remove event
            _context.Events.Remove(eventToDelete);

            //if connected venue, remove event from its list of events.
            if (venue != null)
            {
                venue.Events.Remove(eventToDelete);
            }

            //if its the last event at venue, remove the venue
            if (venue != null && venue.Events.Count == 0)
            {
                _context.Venues.Remove(venue);
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<Event?> GetEventFromDbAsync(EventCreateDTO PossibleEventInDb)
        {
            //Returns Event from db if it has same title and date if it's at the same Venue.
            return await _context.Events.Include(e => e.Venue).FirstOrDefaultAsync(e => e.Title == PossibleEventInDb.Title && e.Date == PossibleEventInDb.Date && e.Venue.Name == PossibleEventInDb.Venue.Name);
        }
    }
}
