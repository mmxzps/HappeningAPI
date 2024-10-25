using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Attraction
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("test")]
        public bool Test { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }
    }

}
