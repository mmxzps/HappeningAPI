using EventVault.Models.DTOs;
using EventVault.Models.ViewModels;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("[Controller]API")]
    [ApiController]
    public class VisitStockholmController : Controller
    {
        private readonly IVisitStockholmServices _visitStockholmServices;
        private readonly string _baseUrl = "https://www.visitstockholm.se/api/v1";
        //private string result;

        public VisitStockholmController(IVisitStockholmServices visitStockholmServices) {
            _visitStockholmServices = visitStockholmServices;
        }

        [HttpGet]
        [Route("getEvents")]
        public async Task<IEnumerable<EventViewModel>> GetEvents()
        {
            var results = await _visitStockholmServices.GetResponse($"{_baseUrl} eventdates /? date_from = &date_to = &one_time = true & categories = music");

            return results;
        }

    }
}
