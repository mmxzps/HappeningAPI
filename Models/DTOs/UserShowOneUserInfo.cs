using System.Text.Json.Serialization;

namespace EventVault.Models.DTOs
{
    public class UserShowOneUserInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("nickName")]
        public string NickName { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("friendships")]
        public object Friendships { get; set; }

        [JsonPropertyName("events")]
        public object Events { get; set; }
    }
}
