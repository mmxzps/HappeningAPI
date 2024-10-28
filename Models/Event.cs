using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventVault.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string EventId { get; set; }
        public string Category { get; set; }

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [ForeignKey("Venue")]
        public int FK_Venue;
        public Venue Venue { get; set; }
        
        //if event runs several dates
        public List<DateTime> Dates = new List<DateTime>();

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool requiresTickets { get; set; }
        public bool ticketsAreAvaliable { get; set; }

        //for pricerange

        public Decimal HighestPrice { get; set; }
        public Decimal LowestPrice { get; set; }
        public string APIEventUrlPage { get; set; }
        public string EventUrlPage { get; set; }


    }
}
