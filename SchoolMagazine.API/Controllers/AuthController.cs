using AutoMapper;
using Microsoft.AspNet.Identity;
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
using System.Net;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _userService.RegisterUserAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _userService.LoginUserAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        //[HttpGet("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail(string email, string token)
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
        //    {
        //        return BadRequest(new { message = "Invalid email or token." });
        //    }

        //    token = WebUtility.UrlDecode(token); // Decode the token

        //    var result = await _userService.ConfirmEmailAsync(email, token);
        //    if (!result)
        //    {
        //        return BadRequest(new { message = "Invalid or expired token." });
        //    }

        //    return Ok(new { message = "Email confirmed successfully!" });
        //}

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Invalid email or token." });
            }

            var result = await _userService.ConfirmEmailAsync(email, token);
            if (!result)
            {
                return BadRequest(new { message = "Invalid or expired token." });
            }

            return Ok(new { message = "Email confirmed successfully!" });
        }



        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            var response = await _userService.ForgotPasswordAsync(request.Email);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await _userService.ResetPasswordAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("enable-2fa/{userId}")]
        public async Task<IActionResult> EnableTwoFactorAuthentication(string userId)
        {
            var result = await _userService.EnableTwoFactorAuthenticationAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-2fa")]
        public async Task<IActionResult> VerifyTwoFactorCode([FromBody] TwoFactorDto model)
        {
            var result = await _userService.VerifyTwoFactorCodeAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }

}
