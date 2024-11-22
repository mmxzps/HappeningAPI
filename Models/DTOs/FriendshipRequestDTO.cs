namespace EventVault.Models.DTOs
{
    public class FriendshipRequestDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public UserGetAllDTO User { get; set; }

        public string FriendId { get; set; }
        public UserGetAllDTO Friend { get; set; }

        //public FriendshipStatus Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
