using Azure.Core;
using EventVault.Models.ViewModels;
using EventVault.Services.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace EventVault.Services
{
    public class KBEventServices : IKBEventServices
    {
        private readonly HttpClient _httpClient;

        public KBEventServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

         readonly string apiKey = "";

        public async Task<KBEventViewModel> GetEventDataAsync(int eventId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://kulturbiljetter.se/api/v3/events/{eventId}");
            string eTag = null;
            

            request.Headers.Add("Authorization", $"Token {apiKey}");

            if (!string.IsNullOrEmpty(eTag)) 
            {
                request.Headers.Add("If-None-Match", $"{eTag}");
            }
            

            var response = await _httpClient.SendAsync(request);
            
            
            if (response.StatusCode == HttpStatusCode.NotModified)
            {
                return null; // No changes
            }

            if (response.IsSuccessStatusCode) 
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonString);
                var eventViewModel = JsonConvert.DeserializeObject<KBEventViewModel>(jsonString);

                

                return eventViewModel;
            }
            else
            {
                throw new Exception($"Failed to fetch data. Error {response.StatusCode}");
            }
            
            
        }

        public async Task<IEnumerable<KBEventListViewModel>> GetListOfEventsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://kulturbiljetter.se/api/v3/events/");

            

            request.Headers.Add("Authorization", $"Token {apiKey}");

            var response = await _httpClient.SendAsync(request);
           

            if (response.IsSuccessStatusCode)
            {
                
                var jsonData = await response.Content.ReadAsStringAsync();

                var parsedObject = JObject.Parse(jsonData);

                //var eventList = JsonConvert.DeserializeObject<List<KBEventViewModel>>(jsonData);

                var eventList = parsedObject.Properties()
                                            .Select(prop => prop.Value.ToObject<KBEventListViewModel>())
                                            .ToList();

                return eventList;
            }
            else
            {
                throw new Exception($"Failed to fetch data. Error {response.StatusCode}");
            }
        }
    }
}
