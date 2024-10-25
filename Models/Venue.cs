using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventVault.Models
{
    public class Venue
    {
        [Key]
        int Id;

        [ForeignKey("Restaurant")]
        List<int>EventId = new List<int>();

        List<Event> EventsAtVenue = new List<Event>();

        string Name;

        string Street;

        string City;

    }
}
