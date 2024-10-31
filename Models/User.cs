using Microsoft.AspNetCore.Identity;

namespace EventVault.Models
{
    public class User : IdentityUser
    {
        public string? NickName { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Event>? Events { get; set; }

    }
}
