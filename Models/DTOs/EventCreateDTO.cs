namespace EventVault.Models.DTOs
{
    public class EventCreateDTO
    {
        public string EventId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string APIEventUrlPage { get; set; }
        public string EventUrlPage { get; set; }

        //if event runs several dates
        public DateTime Date { get; set; }
        public DateTime? TicketsRelease { get; set; }

        //for pricerange
        public Decimal HighestPrice { get; set; }
        public Decimal LowestPrice { get; set; }

        //Venue
        public VenueCreateDTO Venue { get; set; }
    }
}
