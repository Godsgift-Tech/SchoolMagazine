//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using SchoolMagazine.Application.AppInterface;
//using SchoolMagazine.Application.AppUsers.AUTH;
//using SchoolMagazine.Domain.Service_Response;
//using SchoolMagazine.Domain.UserRoleInfo;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SchoolMagazine.Application.AppService
//{
//    public class UserService : IUserService
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly ITokenService _tokenService;
//        private readonly SignInManager<User> _signInManager;
//        private readonly ILogger<UserService> _logger;

//        public UserService(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager, ILogger<UserService> logger)
//        {
//            _userManager = userManager;
//            _tokenService = tokenService;
//            _signInManager = signInManager;
//            _logger = logger;
//        }

//        public async Task<User> DeleteUser(Guid id)
//        {
//            var user = await GetUserById(id);
//            if (user == null) return null;
//            await _userManager.DeleteAsync(user);
//            return user;
//        }

//        public async Task<string> ForgotPassword(string email)
//        {
//            var user = await _userManager.FindByEmailAsync(email);
//            if (user != null)
//            {
//                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//                return token;
//            }
//            return "Cloud not send link to your email";
//        }

//        public async Task<IEnumerable<User>> GetAllUser(int? pageIndex = 1, int? pageSize = 10)
//        {
//            pageIndex = (pageIndex < 1 || !pageIndex.HasValue) ? 1 : pageIndex.Value;
//            pageSize = (pageSize <= 1 || !pageSize.HasValue) ? 10 : pageSize.Value;

//            var users = _userManager.Users;

//            return await users.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
//        }

//        public async Task<User> GetUserById(Guid id)
//        {
//            var user = await _userManager.FindByIdAsync(id.ToString());
//            if (user == null) return null;
//            return user;
//        }

//        public async Task<ApiResponse<LoginResponse>> LoginUser(LoginRequestDto signIn)
//        {
//            User user = null;

//            user = await _userManager.FindByEmailAsync(signIn.Username);

//            if (user == null) return new ApiResponse<LoginResponse>
//            {
//                Status = false,
//                StatusCode = 404,
//                Message = "User does not exist"
//            };


//            var result = await _signInManager.PasswordSignInAsync(signIn.Username, signIn.Password, false, false);
//            if (!result.Succeeded)
//            {
//                return new ApiResponse<LoginResponse>
//                {
//                    Status = false,
//                    StatusCode = 400,
//                    Message = "Incorrect details"
//                };
//            }
//            var roles = await _userManager.GetRolesAsync(user);
//            var token = _tokenService.GenerateToken(user, roles.ToList());
//            return new ApiResponse<LoginResponse>
//            {
//                Status = true,
//                StatusCode = 200,
//                Message = "User logged in successfully",
//                Response = new LoginResponse()
//                {
//                    Token = token,
//                }
//            };
//        }

//        public async Task<ApiResponse<UserResponse>> RegisterUser(RegisterRequestDto signUp)
//        {
//            try
//            {
//                var userExist = await _userManager.FindByEmailAsync(signUp.Username);
//                if (userExist != null)
//                {
//                    return new ApiResponse<UserResponse>
//                    {
//                        Status = false,
//                        StatusCode = 403,
//                        Message = "User already exists!"
//                    };
//                }

//                User user = new User()
//                {
//                    //FirstName = signUp.FirstName,
//                    //LastName = signUp.LastName,
//                    UserName = signUp.Username,  // the Email
//                    Email = signUp.Username,
//                    EmailConfirmed = false
//                };

//                var result = await _userManager.CreateAsync(user, signUp.Password);

//                if (!result.Succeeded)  // 🔹 UPDATED ERROR HANDLING
//                {
//                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
//                    _logger.LogError($"User registration failed: {errors}");

//                    return new ApiResponse<UserResponse>
//                    {
//                        Status = false,
//                        StatusCode = 400,
//                        Message = $"User registration failed: {errors}"
//                    };
//                }

//                var userResult = await _userManager.AddToRoleAsync(user, "User");
//                if (!userResult.Succeeded)
//                {
//                    return new ApiResponse<UserResponse>
//                    {
//                        Status = false,
//                        StatusCode = 400,
//                        Message = "Couldn't assign user a role"
//                    };
//                }

//                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//                return new ApiResponse<UserResponse>
//                {
//                    Status = true,
//                    StatusCode = 201,
//                    Message = $"User created successfully, please check your email {user.Email} for verification.",
//                    Response = new UserResponse
//                    {
//                        User = user,
//                        Token = token
//                    }
//                };
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error authenticating user: {ex.Message} {ex.StackTrace}");
//                throw;
//            }
//        }

//        public async Task<string> ResetPassword(ResetPasswordModel restPassword)
//        {
//            var user = await _userManager.FindByEmailAsync(restPassword.Email);
//            if (user != null)
//            {
//                var restPass = await _userManager.ResetPasswordAsync(user, restPassword.Token, restPassword.Email);
//                if (!restPass.Succeeded)
//                {
//                    return "Failed to change password";
//                }
//            }
//            return "Password has been changed";
//        }

//        public async Task<User> UpdateUser(User user, Guid id)
//        {
//            var existingUser = await GetUserById(id);
//            if (existingUser == null) return null;

//            existingUser.Email = user.Email;
//            existingUser.FirstName = user.FirstName;
//            existingUser.LastName = user.LastName;

//            await _userManager.UpdateAsync(existingUser);
//            return user;
//        }
//    }
//}
