using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class UpcomingEvents
    {
        [JsonPropertyName("mfx-se")]
        public int MfxSe { get; set; }

        [JsonPropertyName("_total")]
        public int Total { get; set; }

        [JsonPropertyName("_filtered")]
        public int Filtered { get; set; }
    }

}
