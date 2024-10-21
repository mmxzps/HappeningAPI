using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Models.ViewModels
{
        public class KBEventListViewModel 
        {
            public int event_id { get; set; }
            public int organizer_id { get; set; }
            public string ETag { get; set; }
        }
}
