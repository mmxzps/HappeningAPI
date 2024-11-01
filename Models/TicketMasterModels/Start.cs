using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Start
    {
        [JsonPropertyName("localDate")]
        public string LocalDate { get; set; }

        [JsonPropertyName("localTime")]
        public string LocalTime { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime? DateTime { get; set; }

        [JsonPropertyName("dateTBD")]
        public bool DateTBD { get; set; }

        [JsonPropertyName("dateTBA")]
        public bool DateTBA { get; set; }

        [JsonPropertyName("timeTBA")]
        public bool TimeTBA { get; set; }

        [JsonPropertyName("noSpecificTime")]
        public bool NoSpecificTime { get; set; }
    }

}
