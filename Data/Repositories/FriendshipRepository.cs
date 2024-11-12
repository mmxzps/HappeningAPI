using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventVault.Data.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly EventVaultDbContext _dbContext;
        public FriendshipRepository(EventVaultDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendFriendRequest(string userId, string friendId)
        {
            var newFriendShip = new Friendship
            {
                UserId = userId,
                FriendId = friendId,
                Status = FriendshipStatus.Pending,
            };
            _dbContext.Friendships.Add(newFriendShip);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AcceptFriendRequest(int friendshipId)
        {
            var request = await _dbContext.Friendships.FindAsync(friendshipId);
            if (request != null && request.Status == FriendshipStatus.Pending)
            {
                request.Status = FriendshipStatus.Accepted;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeclineFriendRequest(int friendshipId)
        {
            var request = await _dbContext.Friendships.FindAsync(friendshipId);
            if (request != null && request.Status == FriendshipStatus.Pending)
            {
                request.Status = FriendshipStatus.Declined;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> ShowAllFriends(string userId)
        {
            var friends = await _dbContext.Friendships
                .Include(x=>x.Friend)
                .Where(x =>  x.UserId == userId && x.Status == FriendshipStatus.Accepted || x.FriendId == userId && x.Status == FriendshipStatus.Accepted)
                .Select(x => x.Friend).ToListAsync();
            return friends;
        }

        public async Task<IEnumerable<Friendship>> ShowFriendshipRequests(string userId)
        {
            var requests = await _dbContext.Friendships.Where(x => x.FriendId == userId && x.Status == FriendshipStatus.Pending).ToListAsync();

            return requests;
        }
    }
}
