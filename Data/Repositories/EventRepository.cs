using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventVaultDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _ticketMasterApiKey;
        public EventRepository(EventVaultDbContext context, HttpClient httpClient)
        {

            _context = context;
            _httpClient = httpClient;
            _ticketMasterApiKey = Environment.GetEnvironmentVariable("TicketmasterApiKey");
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
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

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EventHolder> GetEventInCityAsync(string city)
        {

            try
            {
                string apiUrl = $"https://app.ticketmaster.com/discovery/v2/events?apikey={_ticketMasterApiKey}&locale=*&city={city}";
                var apiResponse = await _httpClient.GetAsync(apiUrl);

                apiResponse.EnsureSuccessStatusCode();

                var content = await apiResponse.Content.ReadAsStringAsync();
                var eventHolder = JsonSerializer.Deserialize<EventHolder>(content);

                return eventHolder;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
