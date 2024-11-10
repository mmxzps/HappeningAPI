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

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var theUser = await _userService.GetOneUserByIdAsync(id);
            return Ok(theUser);
        }

        [HttpGet("GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var theUser = await _userService.GetOneUserByUserNameAsync(userName);
            return Ok(theUser);
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string userId, UserUpdateDTO userUpdateDTO)
        {
            await _userService.UpdateUserAsync(userId, userUpdateDTO);
            return Ok();
        }
    }
}
