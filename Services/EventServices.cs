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
    }
}
