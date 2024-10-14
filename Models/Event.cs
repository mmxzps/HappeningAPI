using Microsoft.AspNetCore.Mvc;

namespace EventVault.Models
{
    public class Event : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
