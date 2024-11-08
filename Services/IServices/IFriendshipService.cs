namespace EventVault.Services.IServices
{
    public interface IFriendshipService
    {
        Task SendFriendRequest(string userId, string friendId);
        Task AcceptFriendRequest(int friendshipId);
        Task DeclineFriendRequest(int friendshipId);
    }
}