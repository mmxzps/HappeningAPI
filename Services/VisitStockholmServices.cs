using Azure;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Services.IServices;
using System.Net.Http;
using System.Text.Json;

namespace EventVault.Services
{
    public class VisitStockholmServices : IVisitStockholmServices
    {
        private readonly IEventRepository _eventRepository;

        public VisitStockholmServices(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
  
        }
        public async Task<IEnumerable<Event>> GetResponse(string baseUrl)
        {
            var _httpclient = new HttpClient();
            var jsonResponse = await _httpclient.GetStringAsync("https://www.visitstockholm.se/api/public-v1/occurrences/?date_from=&date_to=&one_time=true&categories=clubs-parties&categories=music");
            var visitStockholmResponse = JsonSerializer.Deserialize<VisitStockholmEventResponse>(jsonResponse);

            Console.WriteLine(jsonResponse);

            // Return the list of events
            return visitStockholmResponse.Results;
        }
    }
}
