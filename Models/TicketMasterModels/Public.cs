using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Public
    {
        [JsonPropertyName("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("startTBD")]
        public bool StartTBD { get; set; }

        [JsonPropertyName("startTBA")]
        public bool StartTBA { get; set; }

        [JsonPropertyName("endDateTime")]
        public DateTime EndDateTime { get; set; }
    }

}
