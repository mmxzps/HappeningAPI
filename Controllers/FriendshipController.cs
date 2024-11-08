using EventVault.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpPost("SendFriendRequest")]
        public async Task<IActionResult> SendRequest(string userId,  string friendId)
        {
            await _friendshipService.SendFriendRequest(userId, friendId);
            return Ok($"Request sent!");
        }

        [HttpPost("AcceptFriendRequest")]
        public async Task<IActionResult> AcceptRequest(int friendshipId)
        {
            await _friendshipService.AcceptFriendRequest(friendshipId);
            return Ok($"Friend request Accepted!");
        }

        [HttpPost("DeclineFriendRequest")]
        public async Task<IActionResult> DeclineRequest(int friendshipId)
        {
            await _friendshipService.DeclineFriendRequest(friendshipId);
            return Ok($"Friend request declined!");
        }
    }
}
