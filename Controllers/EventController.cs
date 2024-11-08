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

        [HttpGet("allEvents")]
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
            var eventById = await _eventServices.GetEventAsync(id);

            if (eventById != null)
            {
                return Ok(eventById);
            }

            else
            {
                return NotFound("No event with that Id in db.");
            }
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> AddEvent( EventCreateDTO eventCreateDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _eventServices.AddEventAsync(eventCreateDTO);

            return Created();
        }


        //[Authorize(Roles = "Admin")] 
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventUpdateDTO eventUpdateDTO)
        {
            if (eventUpdateDTO.Id != id)
            {
                return BadRequest();
            }

            var result = await _eventServices.UpdateEventAsync(eventUpdateDTO);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();

        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventServices.DeleteEventAsync(id);

            if (!result)
            {
                return  BadRequest();
            }

            return NoContent();
        }
    }
    
}
