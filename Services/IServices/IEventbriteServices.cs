namespace EventVault.Services.IServices
{
    public interface IEventbriteServices
    {
        Task<string> GetEventsAsync();
        Task<string> GetEventByIdAsync(string eventId);
    }
}
