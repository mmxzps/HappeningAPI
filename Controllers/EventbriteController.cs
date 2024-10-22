using EventVault.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventbriteController : ControllerBase
    {
        private readonly IEventbriteServices _eventbriteServices;

        public EventbriteController(IEventbriteServices eventbriteServices)
        {
            _eventbriteServices = eventbriteServices;
        }

        [HttpGet]
        [Route("getEventsByEventbrite")]
        public async Task<IActionResult> GetAllEvents(int page = 1, int pageSize = 10)
        {
            var events = await _eventbriteServices.GetAllEventsAsync(page, pageSize);
            return Ok(events);
        }

        [HttpGet]
        [Route("getEventsByEventbrite/{eventId}")]
        public async Task<IActionResult> GetEventById(string eventId)
        {
            var eventDetail = await _eventbriteServices.GetEventByIdAsync(eventId);
            return Ok(eventDetail);
        }
    }
}
