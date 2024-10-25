using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Promoter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
