using SchoolMagazine.Application.AppUsers.AUTH;
using SchoolMagazine.Domain.Service_Response;
using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IUserService
    {
        
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
        Task<string> GenerateTwoFactorTokenAsync(User user);
        Task<bool> VerifyTwoFactorTokenAsync(User user, string token);
    }
}