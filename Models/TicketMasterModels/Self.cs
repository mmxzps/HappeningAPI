using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Self
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

}
