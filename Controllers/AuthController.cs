using EventVault.Models.DTOs.Identity;
using EventVault.Services;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IRoleServices _roleServices;
        private readonly IEmailService _emailService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IAuthServices authServices, IRoleServices roleServices, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService emailService)
        {
            _authServices = authServices;
            _roleServices = roleServices;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userUsername = await _authServices.GetUserByUsernameAsync(registerDTO.UserName);
            var userEmail = await _authServices.GetUserByEmailAsync(registerDTO.Email);

            if (userEmail!=null)
            {
                return BadRequest("Email is already taken");
            }

            if (userUsername != null)
            {
                return BadRequest("Username is already taken");
            }

            var user = new IdentityUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { token = emailConfirmToken, email = user.Email }, Request.Scheme);
                
                Console.WriteLine("Attempting to send confirmation email...");
                var emailSent = await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                if (!emailSent)
                {
                    Console.WriteLine("Failed to send confirmation email.");
                    return BadRequest("User registered, but failed to send confirmation email.");
                }

                return Ok("User registered successfully. Please confirm your email.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authServices.GetUserByUsernameAsync(loginDTO.UserName);
            if (user == null || !await _authServices.Login(loginDTO))
            {
                return BadRequest("Invalid username or password.");
            }

            var tokenString = await _authServices.GenerateToken(user);
            return Ok(tokenString);
        }

        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _authServices.GetUserByEmailAsync(email);

            if (user == null)
            {
                return BadRequest("Email invalid");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return BadRequest("Email confirmation failed");
            }

            return Ok("Email confirmed");
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _authServices.GetUserByUsernameAsync(forgotPasswordDTO.UserName);
            if (user == null)
            {
                return Ok("If an account with that username exists, a password reset link has been sent.");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("User found, but email address is not set.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return Ok("If an account with that username exists, a password reset link has been sent.");
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordDTO.UserId);
            if (user == null)
            {
                return BadRequest("Invalid user.");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("google-login")]
        public async Task<IActionResult> GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "Auth");

            var props = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            return new ChallengeResult("Google", props);
        }

        [HttpGet]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

            if (!result.Succeeded)
            {
                return BadRequest("Google authentication failed");
            }

            var userInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (userInfo == null)
            {
                return BadRequest("Could not retrieve user information from Google.");
            }

            var email = userInfo.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _authServices.GetUserByEmailAsync(email);

            if (user == null)
            {
                user = new IdentityUser
                {
                    Email = email,
                    UserName = email 
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    return BadRequest($"Could not create user in the database: {errors}");
                }
            }

            var token = await _authServices.GenerateToken(user);
            return Ok(new { token });
        }

    }
}
