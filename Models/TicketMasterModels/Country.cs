using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
    }

}
