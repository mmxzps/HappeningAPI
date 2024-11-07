using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventVault.Models.DTOs
{
    public class EventCreateDTO
    {
        [JsonPropertyName("eventId")]
        public string? EventId { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("apiEventUrlPage")]
        public string? APIEventUrlPage { get; set; }

        [JsonPropertyName("eventUrlPage")]
        public string? EventUrlPage { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("ticketsRelease")]
        public DateTime? TicketsRelease { get; set; }

        [JsonPropertyName("highestPrice")]
        public Decimal? HighestPrice { get; set; }

        [JsonPropertyName("lowestPrice")]
        public Decimal? LowestPrice { get; set; }

        [JsonPropertyName("venue")]
        public VenueCreateDTO? Venue { get; set; }
    }
}
