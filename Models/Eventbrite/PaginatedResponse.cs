namespace EventVault.Models.Eventbrite
{
    public class PaginatedResponse<T>
    {
        public Pagination Pagination { get; set; }
        public List<T> Events { get; set; }
    }
}
