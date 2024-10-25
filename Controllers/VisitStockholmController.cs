using EventVault.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class VisitStockholmController : Controller
    {
        private readonly IVisitStockholmServices _visitStockholmServices;
        private readonly string _baseUrl = "https://www.visitstockholm.se/api/v1";
        private string result;

        public VisitStockholmController(IVisitStockholmServices visitStockholmServices) {
            _visitStockholmServices = visitStockholmServices;
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            var results = await _visitStockholmServices.GetResponse($"{_baseUrl} eventdates /? date_from = &date_to = &one_time = true & categories = music");



            return results;
        }

    }
}
