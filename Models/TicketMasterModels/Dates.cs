using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Dates
    {
        [JsonPropertyName("start")]
        public Start Start { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("spanMultipleDays")]
        public bool SpanMultipleDays { get; set; }
    }

}
