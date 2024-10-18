using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Embedded
    {
        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }

        [JsonPropertyName("venues")]
        public List<Venue> Venues { get; set; }

        [JsonPropertyName("attractions")]
        public List<Attraction> Attractions { get; set; }
    }

}
