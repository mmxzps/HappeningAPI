using Microsoft.AspNetCore.Mvc;
using EventVault.Services.IServices;
using EventVault.Services;

namespace EventVault.Controllers
{
    [Route("[Controller]API")]
    [ApiController]
    public class TicketMasterController : Controller
    {
        private readonly ITicketMasterServices _ticketMasterServices;
        public TicketMasterController(ITicketMasterServices ticketMasterServices){
            _ticketMasterServices = ticketMasterServices;
        }

        [HttpGet]
        [Route("getEvents")]
        public async Task<IActionResult> GetEventsInCity()
        {
            string city = "Stockholm";

            try
            {
                var eventt = await _ticketMasterServices.GetEventsInCityAsync(city);
                return Ok(eventt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
