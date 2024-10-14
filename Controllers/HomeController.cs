using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
