using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using EventVault.Models.ViewModels;
using MimeKit.Cryptography;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Services.IServices
{
    public class EventServices : IEventServices
    {
        private readonly IEventRepository _eventRepository;

        public EventServices(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventGetDTO>> GetAllEventsAsync()
        {
            var eventsList = await _eventRepository.GetAllEventsAsync();

            var eventDTOsList = eventsList.Select(e => new EventGetDTO
            {
                Id = e.Id,
                EventId = e.EventId,
                Category = e.Category,
                Title = e.Title,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                APIEventUrlPage = e.APIEventUrlPage,
                EventUrlPage = e.EventUrlPage,
                Dates = e.Dates,
                TicketsRelease = e.TicketsRelease,
                HighestPrice = e.HighestPrice,
                LowestPrice = e.LowestPrice,
                Venue = new VenueGetDTO
                {
                    Id = e.Venue.Id,
                    Name = e.Venue.Name,
                    Address = e.Venue.Address,
                    City = e.Venue.City,
                    LocationLat = e.Venue.LocationLat,
                    LocationLong = e.Venue.LocationLong
                }

            }).ToList();

            return eventDTOsList;
        }

        public async Task<EventGetDTO> GetEventById(int id)
        {
            var eventItem = await _eventRepository.GetEventById(id);

            var eventDTO = new EventGetDTO
            {
                Id = eventItem.Id,
                EventId = eventItem.EventId,
                Category = eventItem.Category,
                Title = eventItem.Title,
                Description = eventItem.Description,
                ImageUrl = eventItem.ImageUrl,
                APIEventUrlPage = eventItem.APIEventUrlPage,
                EventUrlPage = eventItem.EventUrlPage,
                Dates = eventItem.Dates,
                TicketsRelease = eventItem.TicketsRelease,
                HighestPrice = eventItem.HighestPrice,
                LowestPrice = eventItem.LowestPrice,
                Venue = new VenueGetDTO
                {
                    Id = eventItem.Venue.Id,
                    Name = eventItem.Venue.Name,
                    Address = eventItem.Venue.Address,
                    City = eventItem.Venue.City,
                    LocationLat = eventItem.Venue.LocationLat,
                    LocationLong = eventItem.Venue.LocationLong
                }
            };


        }

        public async Task<bool> AddEventToDbAsync(EventCreateDTO eventDTO)
        {
            Event eventToAdd = new Event
            {
                EventId = eventDTO.EventId,
                Category = eventDTO.Category,
                Title = eventDTO.Title,
                Description = eventDTO.Description,
                ImageUrl = eventDTO.ImageUrl,
                APIEventUrlPage = eventDTO.APIEventUrlPage,
                EventUrlPage = eventDTO.EventUrlPage,
                Dates = eventDTO.Dates,
                TicketsRelease = eventDTO.TicketsRelease,
                HighestPrice = eventDTO.HighestPrice,
                LowestPrice = eventDTO.LowestPrice,

                //Check if venue exists before creating new. else add existing

                //Venue = new Models.Venue
                //{
                //    Id = eventItem.Venue.Id,
                //    Name = eventItem.Venue.Name,
                //    Address = eventItem.Venue.Address,
                //    City = eventItem.Venue.City,
                //    LocationLat = eventItem.Venue.LocationLat,
                //    LocationLong = eventItem.Venue.LocationLong
                //}
            };
            return await _eventRepository.AddEventToDbAsync(eventToAdd);
        }

        
    }
}
