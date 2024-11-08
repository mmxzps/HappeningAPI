using EventVault.Models.DTOs;

namespace EventVault.Models.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string EventId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string APIEventUrlPage { get; set; }
        public string EventUrlPage { get; set; }

        //if event runs several dates
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
        public DateTime? ticketsRelease { get; set; }

        //for pricerange
        public Decimal HighestPrice { get; set; }
        public Decimal LowestPrice { get; set; }

        //Venue
        public VenueViewModel Venue { get; set; }
    }
}
