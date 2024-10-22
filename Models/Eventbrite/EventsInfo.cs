using System;

namespace EventVault.Models.Eventbrite
{
    public class EventsInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Venue Venue { get; set; }
        public Category Category { get; set; }
    }
}
