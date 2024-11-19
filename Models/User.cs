using Microsoft.AspNetCore.Identity;

namespace EventVault.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
        public string? ProfilePictureUrl { get; set; } = "https://img.freepik.com/free-vector/mans-face-flat-style_90220-2877.jpg";
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
