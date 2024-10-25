using EventVault.Models.Eventbrite;
using EventVault.Models;
using EventVault.Services.IServices;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

public class EventbriteServices : IEventbriteServices
{
    private readonly HttpClient _httpClient;

    public EventbriteServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("EVENTBRITE_PRIVATE_TOKEN"));
    }

    public async Task<PaginatedResponse<Event>> GetAllEventsAsync(int page = 1, int pageSize = 10)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.eventbriteapi.com/v3/events/search/?page={page}&page_size={pageSize}");

        var response = await _httpClient.SendAsync(request);

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
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://www.eventbriteapi.com/v3/events/{eventId}/");

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        var eventDetail = JsonSerializer.Deserialize<Event>(jsonString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return eventDetail;
    }
}
