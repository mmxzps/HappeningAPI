using Microsoft.AspNetCore.Identity;

namespace EventVault.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
