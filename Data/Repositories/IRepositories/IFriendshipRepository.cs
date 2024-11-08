namespace EventVault.Data.Repositories.IRepositories
{
    public interface IFriendshipRepository
    {
        Task SendFriendRequest(string userId, string friendId);
        Task AcceptFriendRequest(int friendshipId);
        Task DeclineFriendRequest(int friendshipId);
    }
}
