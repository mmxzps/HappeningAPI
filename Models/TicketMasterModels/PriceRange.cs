using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class PriceRange
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("min")]
        public decimal Min { get; set; }

        [JsonPropertyName("max")]
        public decimal Max { get; set; }
    }

}
