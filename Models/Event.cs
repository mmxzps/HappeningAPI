using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventVault.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
    }
}
