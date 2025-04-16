using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppUsers;
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
//using Microsoft.AspNet.Identity;  => this  will make RoleManager and UserManager throw error


namespace SchoolMagazine.Application.AppService
{


    public class UserService : IUserService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, ITokenService tokenService, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<UserResponse> RegisterUserAsync(RegisterRequestDto model)
        {
            // Create the user
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Username,
                UserName = model.Username,
            };

            // Create the user
            var result = await _userManager.CreateAsync(user, model.Password);

            // Check if user creation was successful
            if (!result.Succeeded)
            {
                return new UserResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            // Check if the provided role exists
            if (!string.IsNullOrWhiteSpace(model.Role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleExists)
                {
                    // If role doesn't exist, create it
                    var role = new Role { Name = model.Role };
                    await _roleManager.CreateAsync(role);
                }

                // Assign the role to the user
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Console.WriteLine($"Generated Token: {token}");

            // Build confirmation link
            var confirmationLink = $"{_configuration["FrontendUrl"]}/confirm-email?email={user.Email}&token={WebUtility.UrlEncode(token)}";
            Console.WriteLine($"Confirmation Link: {confirmationLink}");

            // Load and customize email template
            string emailBody;
            try
            {
                emailBody = await _emailService.GetEmailTemplate("EmailConfirmationTemplate.html", confirmationLink, user.UserName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading email template: {ex.Message}");
                return new UserResponse { Success = false, Message = "Failed to load email template." };
            }

            // Send confirmation email
            try
            {
                await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", emailBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return new UserResponse { Success = false, Message = "Failed to send confirmation email." };
            }

            // Response
            return new UserResponse
            {
                Success = true,
                // Token = token,  =>  avoid registration token to be exposed 
                User = new
                {
                    user.UserName,
                    // user.Email
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

            return new UserResponse { Success = true, Token = jwtToken, Message = "Login successful!." };
        }


        public async Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryParameters parameters)
        {
            // Start with all users
            var usersQuery = _userManager.Users.AsQueryable();

            // Optional filters
            if (!string.IsNullOrWhiteSpace(parameters.Email))
                usersQuery = usersQuery.Where(u => u.Email.Contains(parameters.Email));

            if (!string.IsNullOrWhiteSpace(parameters.FirstName))
                usersQuery = usersQuery.Where(u => u.FirstName.ToLower().Contains(parameters.FirstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.LastName))
                usersQuery = usersQuery.Where(u => u.LastName.ToLower().Contains(parameters.LastName.ToLower()));


            // Materialize the filtered query into a list
            var userList = await usersQuery.AsNoTracking().ToListAsync();

            var filteredUsers = new List<User>();

            foreach (var user in userList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (string.IsNullOrWhiteSpace(parameters.Role) || roles.Any(r => r.Equals(parameters.Role, StringComparison.OrdinalIgnoreCase)))

                {
                    filteredUsers.Add(user);
                }
            }

            // Project to DTOs sequentially
            var dtoList = new List<UserDto>();
            foreach (var user in filteredUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                dtoList.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = roles.FirstOrDefault() ?? "None"
                });
            }

            // Apply pagination
            var totalCount = dtoList.Count;
            var pagedItems = dtoList
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

            return new PagedResult<UserDto>
            {
                Items = pagedItems,
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };

        }


        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = roles.FirstOrDefault() ?? "None"
            };
        }

        public async Task<bool> DeleteUserByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }



        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                Console.WriteLine($"User not found for email: {email}");
                return false;
            }

            // Decode token
            var decodedToken = WebUtility.UrlDecode(token);

            // Confirm email
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                Console.WriteLine($"Email confirmation failed for {email}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                return false;
            }

            Console.WriteLine($"Email confirmed successfully for {email}");
            return true;
        }

        public async Task<UserResponse> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new UserResponse { Success = false, Message = "User not found." };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"{_configuration["FrontendUrl"]}/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";

            string emailTemplate;
            try
            {
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "ResetPasswordTemplate.html");
                emailTemplate = await File.ReadAllTextAsync(templatePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading reset password email template: {ex.Message}");
                return new UserResponse { Success = false, Message = "Failed to load reset password email template." };
            }

            emailTemplate = emailTemplate.Replace("{UserName}", user.UserName)
                                         .Replace("{ResetLink}", resetLink);

            try
            {
                await _emailService.SendEmailAsync(user.Email, "Reset Your Password", emailTemplate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending reset password email: {ex.Message}");
                return new UserResponse { Success = false, Message = "Failed to send password reset email." };
            }

            return new UserResponse
            {
                Success = true,
                Message = "Password reset link sent to your email."
            };
        }

        public async Task<UserResponse> ResetPasswordAsync(ResetPasswordDto model)
        {
            //  Find user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserResponse { Success = false, Message = "User not found." };
            }

            // Decode the token
            var decodedToken = WebUtility.UrlDecode(model.Token);

            // Password reset
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
            if (!result.Succeeded)
            {
                return new UserResponse
                {
                    Success = false,
                    Message = "Password reset failed.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            // Personalize the confirmation email template
            string emailTemplate;
            try
            {
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "ResetPasswordTemplate.html");
                emailTemplate = await File.ReadAllTextAsync(templatePath);

                emailTemplate = emailTemplate
                    .Replace("{UserName}", user.UserName ?? user.Email)
                    .Replace("{ResetMessage}", "Your password has been successfully reset.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading or processing the email template: {ex.Message}");

                return new UserResponse
                {
                    Success = true,
                    Message = "Password reset successful, but confirmation email failed to send."
                };
            }

            // Send the confirmation email
            try
            {
                await _emailService.SendEmailAsync(user.Email, "Password Reset Confirmation", emailTemplate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send password reset confirmation email: {ex.Message}");
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

