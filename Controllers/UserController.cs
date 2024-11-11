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


        [HttpPost("events")]
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

        public async Task<IActionResult> GetEvent(int userId, int eventId)
        {
            try
            {
                //get event of user
                //var event = await _userService.GetUsersEventAsync(userId, eventId);
                return Ok();
            }

            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetAllEvents(int userID)
        {
            try
            {
                //get list of events
                //var events = await _userService.GetAllUserEventsAsync(userId);

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteEventFromUser(int userId, int eventId)
        {
            try
            {
                //remove event from user
                //await _userService.RemoveEventFromUserAsync(userId, eventId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
