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
         //private readonly IUserService _userService;
        // private readonly IEmailService _emailService;
        //private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

       // public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, ITokenService tokenService, IUserService userService)
        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_tokenService = tokenService;
            //_userService = userService;
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


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find user by email
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                // Check if password is correct
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (isPasswordValid)
                {
                    // Proceed with login logic (Create JWT token)
                    return Ok("Login successful");
                }
            }

            return BadRequest("Username or password is incorrect");
        }


        //var tokenResponse = await _userService.LoginUser(loginRequestDto);

        //if (tokenResponse == null || !tokenResponse.Status)
        //{
        //    return StatusCode(StatusCodes.Status400BadRequest,
        //        new Response
        //        {
        //            Status = "Error",
        //            Message = tokenResponse.Message,
        //            IsSuccess = false

        //        });
        //}
        //return Ok(tokenResponse);

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = await _userManager.FindByNameAsync(loginRequestDto.Username);
        //    if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
        //    {
        //        return Unauthorized(new { Message = "Invalid username or password" });
        //    }

        //    var userRoles = await _userManager.GetRolesAsync(user);
        //    var token = GenerateJwtToken(user, userRoles);

        //    return Ok(new
        //    {
        //        Token = token,
        //        UserId = user.Id,
        //        Username = user.UserName,
        //        Roles = userRoles
        //    });
        //}








        // private readonly IMapper _mapper;

        //public AuthController(IUserService userService, IEmailService emailService, UserManager<User> userManager, IMapper mapper)
        //{
        //    _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        //    _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        //    _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //}


        // --------------------------------------
        // 🔹 User Registration & Authentication
        // --------------------------------------

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public async  Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        //{
        //    if (model == null)
        //        return BadRequest(new Response { Message = "Invalid registration data.", IsSuccess = false, Status = "Error" });

        //    if (string.IsNullOrWhiteSpace(model.Email))
        //        return BadRequest(new Response { Message = "Email cannot be empty.", IsSuccess = false, Status = "Error" });

        //    var tokenResponse = await _userService.RegisterUser(model);

        //    if (tokenResponse == null || tokenResponse.Response == null)
        //        return BadRequest(new Response { Message = "Registration failed.", IsSuccess = false, Status = "Error" });

        //    var confirmationUrl = Url.Action(nameof(ConfirmEmail), "Auth",
        //        new { token = tokenResponse.Response?.Token, email = model.Email }, Request.Scheme);

        //    var message = new Message(new string[] { model.Email }, "Email Confirmation Link", confirmationUrl);

        //    await _emailService.SendEmailAsync(message);

        //    return Ok(new Response { IsSuccess = true, Message = tokenResponse.Message, Status = "Success" });
        //}

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        //{
        //    var identityUser = new User
        //    {
        //        UserName = registerRequestDto.Username,
        //        Email = registerRequestDto.Username


        //    };

        // var identityResult =   await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
        //    if (identityResult.Succeeded)
        //    {
        //        // add roles to the user
        //        await _userManager.AddToRolesAsync(identityUser, "User");
        //    }
        //}





        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] SignInModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Invalid login request.");

        //    var tokenResponse = await _userService.LoginUser(model);

        //    if (tokenResponse == null || !tokenResponse.Status)
        //        return BadRequest(new Response { Status = "Error", Message = tokenResponse.Message, IsSuccess = false });

        //    return Ok(tokenResponse);
        //}

        // --------------------------------------
        // 🔹 Email Confirmation
        // --------------------------------------

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return BadRequest("Invalid email confirmation request.");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new Response { Status = "Error", Message = "User not found.", IsSuccess = false });

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email confirmation failed.", IsSuccess = false });

            return Ok(new Response { Status = "Success", Message = "Email verified successfully.", IsSuccess = true });
        }

        // --------------------------------------
        // 🔹 User Management
        // --------------------------------------

        //[Authorize(Roles = "User")]
        //[HttpGet("{id:Guid}")]
        //public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        //{
        //    var user = await _userService.GetUserById(id);
        //    if (user == null)
        //        return NotFound("User not found.");

        //    return Ok(_mapper.Map<UserDto>(user));
        //}

        //[Authorize(Roles = "User")]
        //[HttpPut("{id:Guid}")]
        //public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] User user)
        //{
        //    if (user == null)
        //        return BadRequest("Invalid user data.");

        //    var updatedUser = await _userService.UpdateUser(user, id);
        //    return Ok(_mapper.Map<UserDto>(updatedUser));
        //}

        // --------------------------------------
        // 🔹 Password Reset
        // --------------------------------------

        //[AllowAnonymous]
        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPassword([Required] string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        return BadRequest("Email is required.");

        //    var forgotToken = await _userService.ForgotPassword(email);
        //    var resetPasswordUrl = Url.Action(nameof(ResetPassword), "Auth", new { token = forgotToken, email }, Request.Scheme);

        //    var message = new Message(new string[] { email }, "Password Reset Request", resetPasswordUrl);
        //    await _emailService.SendEmailAsync(message);

        //    return Ok(new ApiResponse<string> { Status = true, Message = "Password reset instructions sent to email." });
        //}

        //[AllowAnonymous]
        //[HttpGet("reset-password")]
        //public IActionResult ResetPassword(string email, string token)
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
        //        return BadRequest("Invalid password reset request.");

        //    var model = new ResetPasswordModel { Email = email, Token = token };
        //    return Ok(new { model });
        //}

        //[AllowAnonymous]
        //[HttpPost("change-password")]
        //public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordModel model)
        //{
        //    if (model == null)
        //        return BadRequest("Invalid password change request.");

        //    await _userService.ResetPassword(model);
        //    return Ok(new ApiResponse<string> { Status = true, Message = "Password changed successfully." });
        //}
    }
}
