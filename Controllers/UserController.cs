using EventVault.Models.DTOs;
using EventVault.Services;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _userService.GetAllUsersAsync();
                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("/{userId}/event")]
        public async Task<IActionResult> AddEventToUser(EventCreateDTO eventCreateDTO, int userId)
        {
            try
            {
                //add specific event to user
                //await _userService.AddEventToUser(eventCreateDTO, userId);

                return Ok(eventCreateDTO);
            }

            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("/{userId}/event/{eventId}")]
        public async Task<IActionResult> GetSavedEvent(string userId, int eventId)
        {
            try
            {
                //get event of user
                var eventSaved = await _userService.GetSavedEventAsync(userId,eventId);
                return Ok();
            }

            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/{userId}/event/")]
        public async Task<IActionResult> GetAllSavedEvents(string userId)
        {
            try
            {
                //get list of events
                var events = await _userService.GetAllSavedEventsAsync(userId);

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/{userId}/event/{eventId}")]
        public async Task<IActionResult> DeleteEventFromUser(string userId, int eventId)
        {
            try
            {
                //remove event from user
                await _userService.RemoveEventFromUserAsync(userId, eventId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
