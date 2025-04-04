using Org.BouncyCastle.Asn1.Ocsp;
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

        //Task<UserResponse> RegisterUserAsync(RegisterRequestDto model);

        //Task<ServiceResponse<string>> ConfirmEmailAsync(string email, string token);
        //Task<LoginResponseDto> SendPasswordResetEmailAsync(string email);
        //Task<UserResponse> LoginAsync(LoginRequestDto request);

        //Task<LoginResponseDto> EnableTwoFactorAsync(string email);

        //Task<ServiceResponse<bool>> VerifyTwoFactorAsync(string email, string token);

        //Task<ServiceResponse<string>> GeneratePasswordResetTokenAsync(string email);
        //Task<ServiceResponse<bool>> ResetPasswordAsync(ResetPasswordDto model);

        Task<UserResponse> RegisterUserAsync(RegisterRequestDto model);
        Task<UserResponse> LoginUserAsync(LoginRequestDto model);
        Task<bool> ConfirmEmailAsync(string email, string token);

        Task<UserResponse> ForgotPasswordAsync(string email);
        Task<UserResponse> ResetPasswordAsync(ResetPasswordDto model);
        Task<UserResponse> EnableTwoFactorAuthenticationAsync(string userId);
        Task<UserResponse> VerifyTwoFactorCodeAsync(TwoFactorDto model);

    }
}