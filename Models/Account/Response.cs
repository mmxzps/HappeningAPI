namespace EventVault.Models.Account
{
    public class Response
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
