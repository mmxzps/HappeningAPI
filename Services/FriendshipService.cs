using EventVault.Data.Repositories.IRepositories;
using EventVault.Models.DTOs;
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

        public async Task<IEnumerable<FriendshipRequestDTO>> ShowFriendshipRequests(string userId)
        {
            var requests = await _friendshipRepository.ShowFriendshipRequests(userId);

            return requests.Select(x => new FriendshipRequestDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                FriendId = x.FriendId,
                CreatedDate = x.CreatedDate,
            });
        }

        public async Task<IEnumerable<UserGetAllDTO>> ShowAllFriends(string userId)
        {
            var allFriends = await _friendshipRepository.ShowAllFriends(userId);

            return allFriends.Select(x => new UserGetAllDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                ProfilePictureUrl = x.ProfilePictureUrl,
                NickName = x.NickName,
                UserName = x.UserName,
            });
        }
    }
}
