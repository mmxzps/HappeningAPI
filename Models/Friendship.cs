namespace EventVault.Models
{
    public class Friendship
    {
        
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string FriendId { get; set; }
        public User Friend { get; set; }

        public FriendshipStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
