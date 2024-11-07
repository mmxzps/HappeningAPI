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
        private readonly IVenueRepository _venueRepository;

        public EventServices(IEventRepository eventRepository, IVenueRepository venueRepository)
        {
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
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
                Date = e.Date,
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

        public async Task<EventGetDTO> GetEventAsync(int id)
        {
            var eventItem = await _eventRepository.GetEventAsync(id);

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
                Date = eventItem.Date,
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
                } ?? new VenueGetDTO()
            } ?? new EventGetDTO { };

            return eventDTO;
        }

        public async Task<bool> AddEventAsync(EventCreateDTO eventDTO)
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
                Date = eventDTO.Date,
                TicketsRelease = eventDTO.TicketsRelease,
                HighestPrice = eventDTO.HighestPrice,
                LowestPrice = eventDTO.LowestPrice,

            };

            //check if venue exists in db, if so add that venue to event. Else,

            var venueToAdd = await _venueRepository.GetVenueIfInDb(eventDTO.Venue.Name, eventDTO.Venue.Address, eventDTO.Venue.City);

            if (venueToAdd == null) 
            {
                //create venue
                venueToAdd = new EventVault.Models.Venue
                {
                    Name = eventDTO.Venue.Name,
                    Address = eventDTO.Venue.Address,
                    City = eventDTO.Venue.City,
                    LocationLat = eventDTO.Venue.LocationLat,
                    LocationLong = eventDTO.Venue.LocationLong
                };

                await _venueRepository.AddVenueAsync(venueToAdd);

                eventToAdd.Venue = venueToAdd;
                eventToAdd.FK_Venue = venueToAdd.Id;
            }

            await _eventRepository.AddEventAsync(eventToAdd);
            return await _venueRepository.AddEventToVenue(eventToAdd, venueToAdd);

        }

        public async Task<bool> UpdateEventAsync(EventUpdateDTO eventDTO)
        {
            if (eventDTO.Title == null || eventDTO.Id == null)
            {
                return false;
            }

            Event? eventToUpdate = await _eventRepository.GetEventAsync(eventDTO.Id);

            if (eventToUpdate != null) 
            {
                eventToUpdate.EventId = eventDTO.EventId;
                eventToUpdate.Category = eventDTO.Category;
                eventToUpdate.Title = eventDTO.Title;
                eventToUpdate.Description = eventDTO.Description;
                eventToUpdate.ImageUrl = eventDTO.ImageUrl;
                eventToUpdate.APIEventUrlPage = eventDTO.APIEventUrlPage;
                eventToUpdate.EventUrlPage = eventDTO.EventUrlPage;
                eventToUpdate.Date = eventDTO.Date;
                eventToUpdate.TicketsRelease = eventDTO.TicketsRelease;
                eventToUpdate.HighestPrice = eventDTO.HighestPrice;
                eventToUpdate.LowestPrice = eventDTO.LowestPrice;

                await _eventRepository.UpdateEventAsync(eventToUpdate);
            }


            return false;
        
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToDelete = await _eventRepository.GetEventAsync(id);

            if (eventToDelete != null) 
            {
                await _eventRepository.DeleteEventAsync(eventToDelete);

                return true;
            }

            else
            {
                return false;
            }
        }
        
    }
}
