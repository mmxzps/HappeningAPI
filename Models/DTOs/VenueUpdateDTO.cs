namespace EventVault.Models.DTOs
{
    public class VenueUpdateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string LocationLat { get; set; }
        public string LocationLong { get; set; }
    }
}
