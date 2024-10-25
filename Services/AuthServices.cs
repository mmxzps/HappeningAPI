using EventVault.Models.DTOs.Identity;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace EventVault.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthServices(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };

            return await _userManager.CreateAsync(identityUser, registerDTO.Password);
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var identityUser = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (identityUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, loginDTO.Password);
        }
    }
}
