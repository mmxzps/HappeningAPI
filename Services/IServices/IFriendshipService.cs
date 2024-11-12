using EventVault.Models.DTOs;

namespace EventVault.Services.IServices
{
    public interface IFriendshipService
    {
        Task SendFriendRequest(string userId, string friendId);
        Task AcceptFriendRequest(int friendshipId);
        Task DeclineFriendRequest(int friendshipId);
        Task<IEnumerable<FriendshipRequestDTO>> ShowFriendshipRequests(string userId);
        Task<IEnumerable<UserGetAllDTO>> ShowAllFriends(string userId);

    }
}