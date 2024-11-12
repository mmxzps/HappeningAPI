using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly EventVaultDbContext _context;
        public UserRepository(UserManager<User> userManager, EventVaultDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUserAsync (string userId)
        {
            var userFromDb = await _context.Users.Include(u => u.Events).FirstOrDefaultAsync(u => u.Id == userId);

            return userFromDb;
        }

        public async Task<Event> GetSavedEventAsync (string userId, int eventId) 
        {
            var userWithEvent = await _context.Users.Include(u => u.Events).ThenInclude(e => e.Venue).FirstOrDefaultAsync(u => u.Id == userId);

            if (userWithEvent == null) 
            {
                return null;
            }

            var eventWithId = userWithEvent.Events.FirstOrDefault(e => e.Id == eventId);

            if (eventWithId == null)
            {
                return null;
            }

            return eventWithId;

        }

        public async Task<IEnumerable<Event>> GetAllSavedEventsAsync (string userId)
        {
            var userWithEvents = await _context.Users
                .Include(u => u.Events)
                .ThenInclude(e => e.Venue)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return userWithEvents.Events;
        }

        public async Task AddEventToUserAsync (string userId, EventCreateDTO eventCreateDTO)
        {
            var userToAdd = await _context.Users.Include(u => u.Events).ThenInclude(e => e.Venue).FirstOrDefaultAsync(u => u.Id == userId);


            if (userToAdd == null) 
            {
                return;
            }

            var eventToAdd = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.Title == eventCreateDTO.Title 
                                       && e.Date == eventCreateDTO.Date 
                                       && e.Venue.Name == eventCreateDTO.Venue.Name);
        
            if (eventToAdd == null)
            {
                return;
            }

            userToAdd.Events.Add(eventToAdd);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveEventFromUserAsync (string userId, int eventId)
        {
            var userWithId = await _context.Users.Include(u => u.Events).ThenInclude(e => e.Venue).FirstOrDefaultAsync(u => u.Id == userId);
            if (userWithId == null) 
            {
                return;
            }
            var eventToRemove = userWithId.Events.FirstOrDefault(e => e.Id == eventId);

            userWithId.Events.Remove(eventToRemove);

            await _context.SaveChangesAsync();  
        }
    }
}

