using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class City
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
