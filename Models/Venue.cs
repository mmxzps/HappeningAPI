using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Street { get; set; }

        public string City { get; set; }

    }
}
