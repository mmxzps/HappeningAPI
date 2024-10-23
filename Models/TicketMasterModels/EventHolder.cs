using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class EventHolder
    {
        [JsonPropertyName("_embedded")]
        public Embedded Embedded { get; set; }

        [JsonPropertyName("page")]
        public Page Page { get; set; }
    }

}
