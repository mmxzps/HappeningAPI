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
        public string APIEventUrlPage { get; set; }
        public string EventUrlPage { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [ForeignKey("Venue")]
        public int FK_Venue;
        public Venue Venue { get; set; }
        
        //if event runs several dates
        public List<DateTime> Dates { get; set; } = new List<DateTime>();

        //releasedate for ticketavaliability
        public DateTime? ticketsRelease {  get; set; }

        //for pricerange

        public Decimal HighestPrice { get; set; }
        public Decimal LowestPrice { get; set; }



    }
}
