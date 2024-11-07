using System.Text.Json.Serialization;

namespace EventVault.Models.DTOs
{
    public class VenueCreateDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("zipCode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("locationLat")]
        public string? LocationLat { get; set; }

        [JsonPropertyName("locationLong")]
        public string? LocationLong { get; set; }
    }
}
