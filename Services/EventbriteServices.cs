using EventVault.Models.Eventbrite;
using EventVault.Models;
using EventVault.Services.IServices;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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

    public async Task<PaginatedResponse<Event>> GetAllEventsAsync(int page = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetAsync($"events/search/?page={page}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var events = JsonSerializer.Deserialize<PaginatedResponse<Event>>(jsonString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        return events;
    }

    public async Task<Event> GetEventByIdAsync(string eventId)
    {
        var response = await _httpClient.GetAsync($"events/{eventId}/");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var eventDetail = JsonSerializer.Deserialize<Event>(jsonString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        return eventDetail;
    }
}
