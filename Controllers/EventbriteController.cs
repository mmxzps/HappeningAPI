using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class EventbriteService
{
    private readonly HttpClient _httpClient;

    public EventbriteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://www.eventbriteapi.com/v3/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_PRIVATE_TOKEN");
    }

    public async Task<string> GetAllEventsAsync()
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

