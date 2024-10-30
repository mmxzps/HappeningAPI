using EventVault.Models.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Services.IServices
{
    public interface IAuthServices
    {
        Task<string> GenerateToken(IdentityUser user);
        Task<bool> Login(LoginDTO loginDTO);
        Task<IdentityResult> Register(RegisterDTO registerDTO);
        Task<IdentityUser> GetUserByUsernameAsync(string username);
    }
}
