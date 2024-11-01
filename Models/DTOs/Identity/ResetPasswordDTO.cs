namespace EventVault.Models.DTOs.Identity
{
    public class ResetPasswordDTO
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
