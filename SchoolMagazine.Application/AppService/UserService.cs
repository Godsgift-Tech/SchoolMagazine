using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppUsers.AUTH;
using SchoolMagazine.Domain.Service_Response;
using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, IEmailService emailService, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toEmail) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentException("Email parameters cannot be empty.");
                }

                await _emailService.SendEmailAsync(toEmail, subject, message);
                _logger.LogInformation($"Email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email to {toEmail}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }

        public async Task<string> GenerateTwoFactorTokenAsync(User user)
        {
            return await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        }

        public async Task<bool> VerifyTwoFactorTokenAsync(User user, string token)
        {
            return await _userManager.VerifyTwoFactorTokenAsync(user, "Email", token);
        }
    }
}

