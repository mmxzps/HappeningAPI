using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Address
    {
        [JsonPropertyName("line1")]
        public string Line1 { get; set; }
    }

}
