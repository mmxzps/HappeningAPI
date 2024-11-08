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

        public async Task<List<EventViewModel>> GetEventsInCityAsync(string city)
        {
            try
            {
                var eventHolder = await GetEventInCityAsync(city);
                if (eventHolder == null || eventHolder.Embedded == null || eventHolder.Embedded.Events == null)
                {
                    throw new Exception("Couldn't find data! Did you spell city name right?");
                }
                //var dto = eventHolder.Embedded.Events.Select(x => new ShowEventDTO
                //{
                //    EventName = x.Name,
                //    EventDate = x.Dates.Start.DateTime,
                //    ImageUrl = x.Images.FirstOrDefault(x => x.Ratio == "3_2").Url,
                //    TicketPurchaseUrl = x.Url,
                //    VenueName = x.Embedded?.Venues.FirstOrDefault()?.Name,
                //    City = x.Embedded.Venues.FirstOrDefault().City.Name,
                //    Address = x.Embedded?.Venues.FirstOrDefault()?.Address.Line1,
                //    MinPrice = x.PriceRanges.FirstOrDefault().Min,
                //    MaxPrice = x.PriceRanges.FirstOrDefault().Max,
                //    Currency = x.PriceRanges.FirstOrDefault().Currency,
                //    Category = x.Classifications.FirstOrDefault().Genre.Name,
                //    SubCategory = x.Classifications.FirstOrDefault().SubGenre.Name,
                //    AvailabilityStatus = x.Dates.Status.Code,
                //});

                var eventViewmodel = eventHolder.Embedded.Events.Select(r => new EventViewModel
                {
                    Title = r.Name,
                    EventId = r.Id,
                    Category = r.Classifications.FirstOrDefault().Genre.Name,

                    //need to find description of event
                    //Description = r....

                    ImageUrl = r.Images.FirstOrDefault(x => x.Ratio == "3_2").Url,
                    EventUrlPage = r.Url,
                    LowestPrice = r.PriceRanges.FirstOrDefault().Min,
                    HighestPrice = r.PriceRanges.FirstOrDefault().Max,

                    Venue = new VenueViewModel
                    {
                        Name = r.Embedded?.Venues.FirstOrDefault().Name,
                        City = r.Embedded?.Venues.FirstOrDefault().City.Name,
                        Address = r.Embedded?.Venues.FirstOrDefault().Address.Line1,
                        ZipCode = r.Embedded?.Venues.FirstOrDefault().PostalCode,
                        LocationLat = r.Embedded.Venues.FirstOrDefault().Location.Latitude,
                        LocationLong = r.Embedded.Venues.FirstOrDefault().Location.Longitude
                    },

                    Dates = r.Dates.Start.DateTime.HasValue ? new List<DateTime> { r.Dates.Start.DateTime.Value } : new List<DateTime>()

                });

                Console.WriteLine("Datetimevalue : " + eventViewmodel.First().Dates[0]);

                return eventViewmodel.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception($"message: {ex.Message}");
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
