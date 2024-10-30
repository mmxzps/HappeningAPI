using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace EventVault.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<DateTime> Dates { get; set; } = new List<DateTime>();

        public bool RequiresTickets { get; set; }

        public bool TicketsAreAvailable { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public string EventUrlPage { get; set; }

        [ForeignKey("Venue")]
        public int FK_Venue { get; set; }

        public Venue Venue { get; set; }
    }
}
