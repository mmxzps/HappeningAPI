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

        public async Task<IEnumerable<EventGetDTO>> GetEventsAsync (int userId)
        {
            var eventsList = await _eventRepository.GetEventsAsync(userId);

            var eventDTOsList = eventsList.Select(e => new EventGetDTO
            {

            }).ToList();

            return eventDTOsList;
        }
    }
}
