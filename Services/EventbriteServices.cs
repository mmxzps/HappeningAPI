using EventVault.Services.IServices;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class EventbriteServices : IEventbriteServices
{
    private readonly HttpClient _httpClient;

    public EventbriteServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://www.eventbriteapi.com/v3/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("EVENTBRITE_PRIVATE_TOKEN"));
    }

    public async Task<string> GetEventsAsync()
    {
        var response = await _httpClient.GetAsync("events/search/");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetEventByIdAsync(string eventId)
    {
        var response = await _httpClient.GetAsync($"events/{eventId}/");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
