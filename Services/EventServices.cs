using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;

namespace EventVault.Services.IServices
{
    public class EventServices : IEventServices
    {
        private readonly IEventRepository _eventRepository;

        public EventServices(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventGetDTO>> GetAllEventsAsync ()
        {
            var eventsList = await _eventRepository.GetAllEventsAsync();

            var eventDTOsList = eventsList.Select(e => new EventGetDTO
            {
                //Add necessary data for eventGetDTO

            }).ToList();

            return eventDTOsList;
        }

        public async Task<bool> AddEventToDbAsync(EventCreateDTO eventDTO)
        {
            Event eventToAdd = new Event
            {
                //add necessary data for Eventobject
            };

            return await _eventRepository.AddEventToDbAsync(eventToAdd);
        }

        public async Task<List<ShowEventDTO>> GetEventInCityAsync(string city)
        {
            var eventHolder = await _eventRepository.GetEventInCityAsync(city);

            var dto = eventHolder.Embedded.Events.Select(x => new ShowEventDTO
            {
                EventName = x.Name,
                EventDate = x.Dates.Start.DateTime,
                ImageUrl = x.Images.FirstOrDefault(x => x.Ratio == "3_2").Url,
                TicketPurchaseUrl = x.Url,
                VenueName = x.Embedded?.Venues.FirstOrDefault()?.Name,
                City = x.Embedded.Venues.FirstOrDefault().City.Name,
                Address = x.Embedded?.Venues.FirstOrDefault()?.Address.Line1,
                MinPrice = x.PriceRanges.FirstOrDefault().Min,
                MaxPrice = x.PriceRanges.FirstOrDefault().Max,
                Currency = x.PriceRanges.FirstOrDefault().Currency,
                Category = x.Classifications.FirstOrDefault().Genre.Name,
                SubCategory = x.Classifications.FirstOrDefault().SubGenre.Name,
                AvailabilityStatus = x.Dates.Status.Code,
            });

            return dto.ToList();
        }
    }
}
