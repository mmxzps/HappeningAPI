using EventVault.Data.Repositories.IRepositories;
using EventVault.Services.IServices;

namespace EventVault.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        public FriendshipService(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        public async Task SendFriendRequest(string userId, string friendId)
        {
            await _friendshipRepository.SendFriendRequest(userId, friendId);
        }
        public async Task AcceptFriendRequest(int friendshipId)
        {
            await _friendshipRepository.AcceptFriendRequest(friendshipId);
        }

        public async Task DeclineFriendRequest(int friendshipId)
        {
            await _friendshipRepository.DeclineFriendRequest(friendshipId);
        }
    }
}
