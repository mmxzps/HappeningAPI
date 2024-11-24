using EventVault.Models.ViewModels;
using EventVault.Services.IServices;
using System.Net.Http;
using System.Text.Json;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Services
{
    public class TicketMasterServices : ITicketMasterServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _ticketMasterApiKey;

        public TicketMasterServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _ticketMasterApiKey = Environment.GetEnvironmentVariable("TicketmasterApiKey");
        }

        public async Task<IEnumerable<EventViewModel>> GetEventsInCityAsync(string city)
        {
            //added to keep list of eventviewmodels and pagenr.
            int pagenr = 0;
            var eventViewmodels = new List<EventViewModel>();
            //------

            try
            {
                EventHolder eventHolder;

                // --------------- added to try and fetch all ticketmaster events: ---------

                do
                {

                    // -------------------------------------------------------------------------

                    eventHolder = await GetEventInCityAsync(city, pagenr);


                    if (eventHolder == null || eventHolder.Embedded == null || eventHolder.Embedded.Events == null)
                    {
                        throw new Exception("Couldn't find data! Did you spell city name right?");
                    }

                    //removed var from eventviewmodels and initialized it above
                    eventViewmodels.AddRange(eventHolder.Embedded.Events.Select(r => new EventViewModel
                    {
                        Title = r.Name ?? "",
                        EventId = r.Id ?? "",
                        Category = r.Classifications != null && r.Classifications
                        .Any(c => c.Genre != null) ?
                        string.Join(", ", r.Classifications.Where(c => c.Segment != null && c.Genre != null).Select(c => c.Segment.Name + " - " + c.Genre.Name))
                        : "",

                        Description = "",
                        //need to find description of event

                        ImageUrl = r.Images?.FirstOrDefault(x => x.Ratio == "3_2")?.Url ?? "",
                        EventUrlPage = r.Url ?? "",
                        LowestPrice = r.PriceRanges?.Min(pr => pr.Min) ?? 0,
                        HighestPrice = r.PriceRanges?.Max(pr => pr.Max) ?? 0,

                        Venue = new VenueViewModel
                        {
                            Name = r.Embedded?.Venues?.FirstOrDefault()?.Name ?? "",
                            City = r.Embedded?.Venues?.FirstOrDefault()?.City?.Name ?? "",
                            Address = r.Embedded?.Venues?.FirstOrDefault()?.Address?.Line1 ?? "",
                            ZipCode = r.Embedded?.Venues?.FirstOrDefault()?.PostalCode ?? "",
                            LocationLat = r.Embedded?.Venues?.FirstOrDefault()?.Location?.Latitude ?? "",
                            LocationLong = r.Embedded?.Venues?.FirstOrDefault()?.Location?.Longitude ?? ""
                        },

                        Dates = r.Dates.Start.DateTime.HasValue ? new List<DateTime> { r.Dates.Start.DateTime.Value } : new List<DateTime>()

                    }));

                    pagenr++;

                } while (eventHolder.Page.Number < eventHolder.Page.TotalPages && pagenr < 5);

                return eventViewmodels;

            }
            catch (Exception ex)
            {
                throw new Exception($"message: {ex.Message}");
            }
        }

        public async Task<EventHolder> GetEventInCityAsync(string city, int page)
        {

            try
            {
                //string apiUrl = $"https://app.ticketmaster.com/discovery/v2/events?apikey={_ticketMasterApiKey}&locale=*&city={city}";
                string apiUrl = $"https://app.ticketmaster.com/discovery/v2/events?apikey={_ticketMasterApiKey}&size=50&page={page}&locale=*&city={city}&sort=date,asc";
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
