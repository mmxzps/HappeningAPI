using EventVault.Models;
using EventVault.Models.DTOs;

namespace EventVault.Data.Repositories.IRepositories
{
    public interface IFriendshipRepository
    {
        Task SendFriendRequest(string userId, string friendId);
        Task AcceptFriendRequest(int friendshipId);
        Task DeclineFriendRequest(int friendshipId);
        Task<IEnumerable<User>> ShowAllFriends(string userId);
        //Task<IEnumerable<UserGetAllDTO>> ShowAllFriends(string userId);
        Task<IEnumerable<Friendship>> ShowFriendshipRequests(string userId);
    }
}
