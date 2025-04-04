using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppUsers.AUTH;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;
using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{


    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, ITokenService tokenService, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _configuration = configuration;
        }


        public async Task<UserResponse> RegisterUserAsync(RegisterRequestDto model)
        {
            // Step 1: Create the user
            var user = new User { FirstName = model.FirstName, LastName = model.LastName, Email = model.Username, UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);

            // Step 2: If user creation failed, return errors
            if (!result.Succeeded)
            {
                return new UserResponse { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() };
            }

            // Step 3: Generate the email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Console.WriteLine($"Generated Token: {token}");

            // Step 4: Build the confirmation link
            var confirmationLink = $"{_configuration["FrontendUrl"]}/confirm-email?email={user.Email}&token={WebUtility.UrlEncode(token)}";
            Console.WriteLine($"Confirmation Link: {confirmationLink}");

            // Step 5: Read email template and replace placeholders
            string emailTemplate;
            try
            {
                emailTemplate = File.ReadAllText("Helpers/EmailConfirmationTemplate.html");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading email template: {ex.Message}");
                return new UserResponse { Success = false, Message = "Failed to load email template." };
            }

            emailTemplate = emailTemplate.Replace("{UserName}", user.UserName)
                                         .Replace("{ConfirmationLink}", confirmationLink);

            // Step 6: Log the final email content (for debugging)
            Console.WriteLine($"Email Template Content: {emailTemplate}");

            // Step 7: Send the confirmation email
            try
            {
                await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", emailTemplate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return new UserResponse { Success = false, Message = "Failed to send confirmation email." };
            }

            // Step 8: Return success response with token and user details
            return new UserResponse
            {
                Success = true,
                Token = token,  // Include the generated token here
                User = new
                {
                    user.UserName,
                    user.Email
                },
                Message = "Registration successful. Check your email for confirmation."
            };
        }



        public async Task<UserResponse> LoginUserAsync(LoginRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new UserResponse { Success = false, Message = "Invalid credentials" };

            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var jwtToken = _tokenService.CreateJWTToken(user, roles);

            return new UserResponse { Success = true, Token = jwtToken, Message = "Login successful!" };
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<UserResponse> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new UserResponse { Success = false, Message = "User not found." };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{_configuration["FrontendUrl"]}/reset-password?email={email}&token={WebUtility.UrlEncode(token)}";

            string emailTemplate = File.ReadAllText("Helpers/ResetPasswordTemplate.html")
                .Replace("{UserName}", user.UserName)
                .Replace("{ResetLink}", resetLink);

            await _emailService.SendEmailAsync(user.Email, "Reset Your Password", emailTemplate);

            return new UserResponse { Success = true, Message = "Reset password link sent to your email." };
        }


        public async Task<UserResponse> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserResponse { Success = false, Message = "User not found." };
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                return new UserResponse
                {
                    Success = false,
                    Message = "Password reset failed.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new UserResponse { Success = true, Message = "Password reset successfully." };
        }

        public async Task<UserResponse> EnableTwoFactorAuthenticationAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new UserResponse { Success = false, Message = "User not found." };
            }

            user.TwoFactorEnabled = true;
            await _userManager.UpdateAsync(user);

            return new UserResponse { Success = true, Message = "Two-factor authentication enabled." };
        }

        public async Task<UserResponse> VerifyTwoFactorCodeAsync(TwoFactorDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserResponse { Success = false, Message = "User not found." };
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, model.Provider, model.Token);
            if (!isValid)
            {
                return new UserResponse { Success = false, Message = "Invalid or expired token." };
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var jwtToken = _tokenService.CreateJWTToken(user, roles);

            return new UserResponse
            {
                Success = true,
                Token = jwtToken,
                User = user,
                Message = "Two-factor authentication successful."
            };
        }

    }


}

