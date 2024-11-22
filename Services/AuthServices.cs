using EventVault.Models;
using EventVault.Models.DTOs.Identity;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace EventVault.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRoleServices _roleServices;

        public AuthServices(UserManager<User> userManager, IConfiguration configuration, IRoleServices roleServices)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleServices = roleServices;
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            var identityUser = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(identityUser, registerDTO.Password);

            if (result.Succeeded)
            {
                await _roleServices.AssignRoleBasedOnUsernameAsync(identityUser);
            }

            return result;
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

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return (User) await _userManager.FindByNameAsync(username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return (User) await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]));
            if (securityKey == null)
            {
                throw new ArgumentNullException("JWT key cannot be null.");
            }

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var issuer = _configuration["JWT_ISSUER"];
            var audience = _configuration["JWT_AUDIENCE"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new ArgumentNullException("Issuer or Audience is null or empty.");
            }

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: issuer,
                audience: audience,
                signingCredentials: signingCred
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
