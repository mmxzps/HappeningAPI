using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Sales
    {
        [JsonPropertyName("public")]
        public Public Public { get; set; }
    }

}
