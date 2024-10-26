using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventVault.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public String Description { get; set; }

        [ForeignKey("Venue")]
        public int FK_Venue;

        public Venue Venue { get; set; }

        //if event runs several dates
        public List<DateTime> Dates = new List<DateTime>();

        public bool requiresTickets { get; set; }

        public bool ticketsAreAvaliable { get; set; }

        //for pricerange

        public Decimal HighestPrice { get; set; }

        public Decimal LowestPrice { get; set; }

        public String EventUrlPage { get; set; }


    }
}
