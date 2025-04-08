using Org.BouncyCastle.Asn1.Ocsp;
using SchoolMagazine.Application.AppUsers.AUTH;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Paging;
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


        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<bool> DeleteUserByIdAsync(Guid userId);
        Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryParameters parameters);
        Task<UserResponse> RegisterUserAsync(RegisterRequestDto model);
        Task<UserResponse> LoginUserAsync(LoginRequestDto model);
        Task<bool> ConfirmEmailAsync(string email, string token);
       // Task<User> FindByEmailAsync(string email);
        Task<UserResponse> ForgotPasswordAsync(string email);
        Task<UserResponse> ResetPasswordAsync(ResetPasswordDto model);
        Task<UserResponse> EnableTwoFactorAuthenticationAsync(string userId);
        Task<UserResponse> VerifyTwoFactorCodeAsync(TwoFactorDto model);

    }
}