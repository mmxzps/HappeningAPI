using EventVault.Models;
using EventVault.Models.DTOs;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EventController : Controller
    {
        private readonly IEventServices _eventServices;

        public EventController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventGetDTO>>> GetAllEvents()
        {
            var events = await _eventServices.GetAllEventsAsync();

            if (events != null)
            {
                // Map Event entity to EventGetDTO
                var eventDTOs = events.Select(e => new EventGetDTO
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

                return Ok(eventDTOs);
            }

            else
            {
                return NotFound("No events in database.");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventGetDTO>> GetEventById(int id)
        {
            var eventById = await _eventServices.GetEventById(id);

            if (eventById != null)
            {
                return Ok(e);
            }

            else
            {
                return NotFound("No event with that Id in db.");
            }
        }
    
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddEventToDb(EventCreateDTO eventCreateDTO)
        {
            var eventToAdd = new EventCreateDTO
            {

                //add whatever is requred in eventobject contains.

            };

            var isSuccessfull = await _eventServices.AddEventToDbAsync(eventCreateDTO);

            if (isSuccessfull)
            {
                return Ok("Event Added");
            }

            else
            {
                return BadRequest();
            }
        }
    }
    
}
