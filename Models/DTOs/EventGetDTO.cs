using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventVault.Models.DTOs
{
    public class EventGetDTO
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
        public List<Date> Dates = new List<Date>();
        public DateTime ticketsRelease { get; set; }
        public bool requiresTickets { get; set; }
        public bool ticketsAreAvaliable { get; set; }

        //for pricerange
        public Decimal HighestPrice { get; set; }
        public Decimal LowestPrice { get; set; }

        //Venue
        public VenueGetDTO Venue { get; set; }
       
    }
}
