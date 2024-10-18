using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Classification
    {
        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("segment")]
        public Segment Segment { get; set; }

        [JsonPropertyName("genre")]
        public Genre Genre { get; set; }

        [JsonPropertyName("subGenre")]
        public SubGenre SubGenre { get; set; }

        [JsonPropertyName("family")]
        public bool Family { get; set; }
    }

}
