using EventVault.Models.DTOs.Identity;
using EventVault.Services;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IRoleServices _roleServices;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IAuthServices authServices, IRoleServices roleServices, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _authServices = authServices;
            _roleServices = roleServices;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authServices.Register(registerDTO);

            if (result.Succeeded)
            {
                var user = await _authServices.GetUserByUsernameAsync(registerDTO.UserName);
                await _roleServices.AssignRoleBasedOnUsernameAsync(user);

                return Ok("User registered successfully with assigned role.");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _authServices.Login(loginDTO))
            {
                var tokenString = _authServices.GenerateToken(loginDTO);
                return Ok(tokenString);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _authServices.GetUserByUsernameAsync(forgotPasswordDTO.UserName);
            if (user == null)
            {
                return Ok("If an account with that username exists, a password reset link has been sent.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Reset Password",
                $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return Ok("If an account with that username exists, a password reset link has been sent.");
        }

        [HttpPost]
        [Route("resetpassword")]
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
    }
}
