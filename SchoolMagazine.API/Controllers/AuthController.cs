using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Application.AppUsers.AUTH;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Application.Email_Messaging;
using SchoolMagazine.Domain.Service_Response;
using SchoolMagazine.Domain.UserRoleInfo;
using System.ComponentModel.DataAnnotations;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService; 

        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, ITokenService tokenService, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _userService = userService;
            _emailService = emailService; //  Assign email service
        }




        [AllowAnonymous]
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Ensure role is provided
                if (string.IsNullOrWhiteSpace(registerRequestDto.Role))
                {
                    return BadRequest(new { Message = "Role is required." });
                }

                // Check if the role exists
                var roleExists = await _roleManager.RoleExistsAsync(registerRequestDto.Role);
                if (!roleExists)
                {
                    return BadRequest(new { Message = "Invalid role specified." });
                }

                var identityUser = new User
                {
                    UserName = registerRequestDto.Username,
                    Email = registerRequestDto.Username,
                    FirstName = registerRequestDto.FirstName,
                    LastName = registerRequestDto.LastName
                   
                  
                };

                var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

                if (identityResult.Succeeded)
                {
                    // Assign the user to the selected role
                    await _userManager.AddToRoleAsync(identityUser, registerRequestDto.Role);

                    return Ok(new { Message = "User registered successfully,Proceed to login"});
                }

                // Return errors if registration fails
                return BadRequest(identityResult.Errors);
            }


      

        //}
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Username or password is incorrect");
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
            {
                return BadRequest("User has no roles assigned.");
            }

            // Generate JWT Token
            var jwtToken = _tokenService.CreateJWTToken(user, roles.ToList());

            // Send the token via email
            var subject = "Your Login Token";
            var body = $"Hello {user.Email},<br/><br/>Your login token:<br/><br/><strong>{jwtToken}</strong><br/><br/>Regards,<br/>School Magazine Team";
            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("User email is missing.");
            }
            try
            {
                
               

                await _emailService.SendEmailAsync(user.Email, subject, body);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return StatusCode(500, $"Error sending email: {ex.Message}");
            }

            return Ok(new { Message = "Login successful! Token has been sent to your email.", Token = jwtToken });
        }


        // AuthController.cs
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null) return BadRequest("User not found");

            var token = await _userService.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://yourfrontend.com/reset-password?email={user.UserName}&token={token}";
            await _userService.SendEmailAsync(user.UserName, "Password Reset", $"Click <a href='{resetLink}'>here</a> to reset your password");

            return Ok("Reset link sent to email");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return BadRequest("Invalid request");

            var result = await _userService.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            return result ? Ok("Password reset successful") : BadRequest("Password reset failed");
        }

        [HttpPost("enable-2fa")]
        public async Task<IActionResult> EnableTwoFactor([FromBody] TwoFactorDto twoFactorDto)
        {
            var user = await _userManager.FindByEmailAsync(twoFactorDto.Email);
            if (user == null) return BadRequest("User not found");

            var token = await _userService.GenerateTwoFactorTokenAsync(user);
            await _userService.SendEmailAsync(user.Email, "2FA Code", $"Your 2FA code is: {token}");

            return Ok("2FA code sent to email");
        }

        [HttpPost("verify-2fa")]
        public async Task<IActionResult> VerifyTwoFactor([FromBody] VerifyTwoFactorDto verifyDto)
        {
            var user = await _userManager.FindByEmailAsync(verifyDto.Email);
            if (user == null) return BadRequest("User not found");

            var isValid = await _userService.VerifyTwoFactorTokenAsync(user, verifyDto.Token);
            return isValid ? Ok("2FA verified") : BadRequest("Invalid 2FA code");
        }

      

    }
}
