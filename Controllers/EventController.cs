using EventVault.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("[Controller]")]
    public class EventController : Controller
    {
        private readonly IEventServices _eventServices;

        public EventController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents (int userId)
        {
            var events = await _eventServices.GetEventsAsync(userId);

            return Ok(events);

        }
    }
}
