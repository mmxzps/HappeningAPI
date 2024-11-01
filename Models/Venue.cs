using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace EventVault.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Event")]
        public List<int>EventId = new List<int>();

        public List<Event> EventsAtVenue = new List<Event>();

        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
    }
}
