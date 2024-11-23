using Azure;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using EventVault.Models.ViewModels;
using EventVault.Services.IServices;
using System.Net.Http;
using System.Text.Json;
using System.Globalization;

namespace EventVault.Services
{
    public class VisitStockholmServices : IVisitStockholmServices
    {
        private readonly IEventRepository _eventRepository;

        public VisitStockholmServices(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
  
        }
        public async Task<IEnumerable<EventViewModel>> GetResponse(string baseUrl)
        {
            var eventList = new List<EventViewModel>();
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
                        var eventViewModel = new EventViewModel
                        {

                            EventId = vsEvent.Id,
                            Category = string.Join(" ", (from row in vsEvent.EventCategories select row.Slug).ToArray()),
                            Title = vsEvent.Title?.En ?? "No title", // Use English title, if null = "No title"
                            Description = vsEvent.Description?.En ?? "", // English description, if null = "No description"
                            APIEventUrlPage = vsEvent.Url ?? "",
                            EventUrlPage = vsEvent.ExternalWebsiteUrl ?? "",
                            ImageUrl = "https://www.visitstockholm.se" + vsEvent.FeaturedImage.Url,

                            Venue = new VenueViewModel
                            {
                                Name = vsEvent.VenueName ?? "",
                                Address = vsEvent.Address ?? "",
                                City = vsEvent.City ?? "",
                                ZipCode = vsEvent.ZipCode ?? ""
                            }
                        };

                        // Parse date
                        bool dateParsed = DateTime.TryParseExact(vsEvent.StartDate, "yyyy-MM-dd",
                                             CultureInfo.InvariantCulture,
                                             DateTimeStyles.None,
                                             out DateTime parsedDate);

                        string[] formats = { "h\\:mm", "hh\\:mm", "h\\:mm\\:ss", "hh\\:mm\\:ss" };
                        // Parse time
                        bool timeParsed = TimeSpan.TryParseExact(vsEvent.StartTime, formats,
                                                                 CultureInfo.InvariantCulture,
                                                                 out TimeSpan parsedTime);

                        if (dateParsed && timeParsed)
                        {
                            DateTime startDate = parsedDate.Add(parsedTime);
                            eventViewModel.Dates.Add(startDate);
                        }

                        if (vsEvent.Location != null)
                        {
                            eventViewModel.Venue.LocationLat = vsEvent.Location.Latitude.ToString() ?? "";
                            eventViewModel.Venue.LocationLong = vsEvent.Location.Longitude.ToString() ?? "";
                        }

                        // Add to the list of DTOs
                        eventList.Add(eventViewModel);
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
