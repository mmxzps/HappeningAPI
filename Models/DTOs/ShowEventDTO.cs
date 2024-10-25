namespace EventVault.Models.DTOs
{
    public class ShowEventDTO
    {
        public string EventName { get; set; } // Namnet på evenemanget
        public DateTime EventDate { get; set; } // Datum och tid för evenemanget
        public string ImageUrl { get; set; } // Länk till bild (väljer en högupplöst)
        public string TicketPurchaseUrl { get; set; } // Länk till biljettköp
        public string VenueName { get; set; } // Namn på lokalen där eventet hålls
        public string City { get; set; } // Stad där lokalen ligger
        public string Address { get; set; } // Adress till lokalen
        public decimal MinPrice { get; set; } // Lägsta pris för biljetter
        public decimal MaxPrice { get; set; } // Högsta pris för biljetter
        public string Currency { get; set; } // Valuta för priser
        public string Category { get; set; } // Genre eller kategori för evenemanget
        public string SubCategory { get; set; } // Subgenre för evenemanget
        public string AvailabilityStatus { get; set; } // Biljettstatus, t.ex. "onsale"
    }
}
