using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Status
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

}
