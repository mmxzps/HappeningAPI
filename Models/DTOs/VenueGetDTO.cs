using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace EventVault.Models.DTOs
{
    public class VenueGetDTO
    {
        public int? Id{ get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? LocationLat { get; set; }
        public string? LocationLong { get; set; }

        public List<EventGetDTO> Events { get; set; } = new List<EventGetDTO>();
    }
}
