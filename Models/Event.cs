using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventVault.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        string Title;

        String Description;


        //if event runs several dates

        List<DateTime> Dates;

        bool requiresTickets;

        bool ticketsAreAvaliable;

        //for pricerange

        Decimal HighestPrice;

        Decimal LowestPrice;

        String EventUrlPage;

        [ForeignKey("Venue")]
        int FK_Venue;

        Venue Venue;
    }
}
