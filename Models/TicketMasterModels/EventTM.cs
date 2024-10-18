using System.Text.Json.Serialization;

namespace TicketmasterTesting.Models.TicketMasterModels
{
    public class EventTM
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("test")]
        public bool Test { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("sales")]
        public Sales Sales { get; set; }

        [JsonPropertyName("dates")]
        public Dates Dates { get; set; }

        [JsonPropertyName("classifications")]
        public List<Classification> Classifications { get; set; }

        [JsonPropertyName("promoter")]
        public Promoter Promoter { get; set; }

        [JsonPropertyName("promoters")]
        public List<Promoter> Promoters { get; set; }

        [JsonPropertyName("priceRanges")]
        public List<PriceRange> PriceRanges { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }

        [JsonPropertyName("_embedded")]
        public Embedded Embedded { get; set; }
    }

}
