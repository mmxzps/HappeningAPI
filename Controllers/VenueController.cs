using EventVault.Models.DTOs;
using EventVault.Services;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VenueController : Controller
    {
        private readonly IVenueServices _venueServices;

        public VenueController(IVenueServices venueServices)
        {
            _venueServices = venueServices;
        }

        [HttpGet("AllVenues")]
        public async Task<ActionResult<IEnumerable<VenueGetDTO>>> GetAllVenues()
        {
            var venues = await _venueServices.GetAllVenuesAsync();

            if (venues != null)
            {
                // Map Event entity to EventGetDTO
                var venuesDTOs = venues.Select(v => new VenueGetDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Address = v.Address,
                    City = v.City,
                    LocationLat = v.LocationLat,
                    LocationLong =  v.LocationLong
                   
                }).ToList();

                return Ok(venuesDTOs);
            }

            else
            {
                return NotFound("No venues in database.");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VenueGetDTO>> GetVenue(int id)
        {
            var venueById = await _venueServices.GetVenueAsync(id);

            if (venueById != null)
            {
                return Ok($"{venueById}");
            }

            else
            {
                return NotFound("No event with that Id in db.");
            }
        }

        [HttpPost("addVenue")]
        public async Task<IActionResult> AddVenue(VenueCreateDTO venueCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _venueServices.AddVenueAsync(venueCreateDTO);

            return Created();
        }

            //[Authorize(Roles = "Admin")] 
            [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVenue(int id, VenueUpdateDTO venueUpdateDTO)
        {
            if (venueUpdateDTO.Id != id)
            {
                return BadRequest();
            }

            var result = await _venueServices.UpdateVenueAsync(venueUpdateDTO);

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
            var result = await _venueServices.DeleteVenueAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
