using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Links
    {
        [JsonPropertyName("self")]
        public Self Self { get; set; }

        [JsonPropertyName("attractions")]
        public List<Attraction> Attractions { get; set; }

        [JsonPropertyName("venues")]
        public List<Venue> Venues { get; set; }
    }

}
