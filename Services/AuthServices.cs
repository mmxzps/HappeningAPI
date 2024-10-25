using EventVault.Models.DTOs.Identity;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

//TODO:

// Add tracing codes, eg. if user has choosen "dark mode" 
// Add logic for admin users as well
// Add "ForgetUserNameOrPassword" method

namespace EventVault.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthServices(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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

        public async Task<string> GenerateToken(LoginDTO loginDTO)
        {
            IEnumerable<Claim> claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, loginDTO.UserName),
            new Claim(ClaimTypes.Role, "User"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT_KEY").Value));

            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken
                (claims:claims, 
                expires: DateTime.Now.AddMinutes(60),
                issuer: _configuration.GetSection("JWT_ISSUER").Value,
                audience: _configuration.GetSection("JWT_AUDIENCE").Value,
                signingCredentials:signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
