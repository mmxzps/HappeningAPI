using EventVault.Services.IServices;
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

        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventbriteServices.GetEventsAsync();
            return Ok(events);
        }

        [HttpGet("events/{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var eventDetails = await _eventbriteServices.GetEventByIdAsync(id);
            return Ok(eventDetails);
        }
    }
}
