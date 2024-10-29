using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace EventVault.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public List<int> EventId { get; set; } = new List<int>();

        public List<Event> EventsAtVenue { get; set; } = new List<Event>();

        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }
    }
}
