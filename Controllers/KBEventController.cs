using EventVault.Models.ViewModels;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KBEventController : ControllerBase
    {
        private readonly IKBEventServices _services;

        public KBEventController(IKBEventServices services)
        {
            _services = services;
        }
            
        [HttpGet]
        [Route("/getEventList")]
        public async Task<ActionResult<IEnumerable<KBEventListViewModel>>> GetEventList()
        {
             var eventList = await _services.GetListOfEventsAsync();

             return Ok(eventList);
        }

        [HttpGet]
        [Route("/getEvent")]
        public async Task<ActionResult<KBEventViewModel>> GetEvent()
        {
            var eventObject = await _services.GetEventDataAsync();

            return Ok(eventObject);
        }


    }
}
