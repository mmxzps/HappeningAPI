using EventVault.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet]
        [Route("users/{searchTerm}")]
        public async Task<IActionResult> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); 
            }

            var users = await _adminServices.SearchUsersAsync(searchTerm);
            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound("No users found matching the search criteria.");
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); 
            }

            var users = await _adminServices.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpDelete]
        [Route("users/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); 
            }

            await _adminServices.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
