using System.ComponentModel.DataAnnotations;

namespace EventVault.Models
{
    public class Date
    {
        [Key]
        int Id { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; } 

        public string ticketUrl { get; set; }
    }
}
