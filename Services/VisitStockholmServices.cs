using Azure;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Models.DTOs;
using EventVault.Models.Eventbrite;
using EventVault.Services.IServices;
using System.Net.Http;
using System.Text.Json;

namespace EventVault.Services
{
    public class VisitStockholmServices : IVisitStockholmServices
    {
        private readonly IEventRepository _eventRepository;

        public VisitStockholmServices(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
  
        }
        public async Task<IEnumerable<EventGetDTO>> GetResponse(string baseUrl)
        {
            var eventList = new List<EventGetDTO>();
            var _httpclient = new HttpClient();
            var startUrl = "https://www.visitstockholm.se/api/public-v1/occurrences/?date_from=&date_to=&one_time=true&categories=clubs-parties&categories=music";
            string nextUrl = startUrl;

            do
            {
                var jsonResponse = await _httpclient.GetStringAsync(nextUrl);
                var visitStockholmResponse = JsonSerializer.Deserialize<VisitStockholmEventResponse>(jsonResponse);


                if (visitStockholmResponse?.Results != null)
                {
                    // Loop through the events from the API response
                    foreach (var vsEvent in visitStockholmResponse.Results)
                    {
                        // Map each Event to EventGetDTO
                        var eventDTO = new EventGetDTO
                        {

                            EventId = vsEvent.Id,
                            Category = string.Join(" ", (from row in vsEvent.EventCategories select row.Slug).ToArray()),
                            //Id = int.Parse(vsEvent.Id), // Assuming the Id is a string, convert to int.
                            Title = vsEvent.Title?.En ?? "No title", // Use English title, if null = "No title"
                            Description = vsEvent.Description?.En ?? "No description", // English description, if null = "No description"
                            APIEventUrlPage = vsEvent.Url ?? "",
                            EventUrlPage = vsEvent.ExternalWebsiteUrl ?? "",
                            ImageUrl = "visitstockholm.se/" + vsEvent.FeaturedImage.Url,
                            StartDate = DateTime.TryParse(vsEvent.StartDate, out var startDate) ? startDate : DateTime.MinValue,
                            EndDate = DateTime.TryParse(vsEvent.EndDate, out var endDate) ? endDate : DateTime.MinValue,
                            StartTime = TimeSpan.TryParse(vsEvent.StartTime, out var startTime) ? startTime : TimeSpan.MinValue,
                            EndTime = TimeSpan.TryParse(vsEvent.EndTime, out var endTime) ? endTime : TimeSpan.MinValue,


                            // No data for these from API :(

                            //requiresTickets = ,
                            //ticketsAreAvaliable = ,
                            //HighestPrice =  , // Set this properly if available in the API response
                            //LowestPrice =     // Set this properly if available in the API response

                            Venue = new VenueGetDTO
                            {
                                Name = vsEvent.VenueName,
                                Address = vsEvent.Address,
                                City = vsEvent.City
                            }
                        };

                        if (vsEvent.Location != null)
                        {
                            eventDTO.Venue.LocationLat = vsEvent.Location.Latitude.ToString() ?? "";
                            eventDTO.Venue.LocationLong = vsEvent.Location.Longitude.ToString() ?? "";
                        }

                        // Add to the list of DTOs
                        eventList.Add(eventDTO);
                    }
                }

                nextUrl = visitStockholmResponse.Next;

            }

            while (!String.IsNullOrEmpty(nextUrl));
            
            //Console.WriteLine(jsonResponse);

            // Return the list of events
            //return visitStockholmResponse.Results;

            return eventList;
        }
    }
}
