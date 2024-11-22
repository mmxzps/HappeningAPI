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
                User = new UserGetAllDTO
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    NickName = x.User.NickName,
                    UserName = x.User.UserName,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    ProfilePictureUrl = x.User.ProfilePictureUrl
                },
                FriendId = x.FriendId,
                Friend = new UserGetAllDTO
                {
                    Id = x.Friend.Id,
                    FirstName = x.Friend.FirstName,
                    LastName = x.Friend.LastName,
                    NickName = x.Friend.NickName,
                    UserName = x.Friend.UserName,
                    Email = x.Friend.Email,
                    PhoneNumber = x.Friend.PhoneNumber,
                    ProfilePictureUrl = x.Friend.ProfilePictureUrl
                },
                CreatedDate = x.CreatedDate,
            });

        }

        public async Task<IEnumerable<UserGetAllDTO>> ShowAllFriends(string userId)
        {
            var allFriends = await _friendshipRepository.ShowAllFriends(userId);

            return allFriends.Select(x => new UserGetAllDTO
            {
                Id = x.Id,
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
