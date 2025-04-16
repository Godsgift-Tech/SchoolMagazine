using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles = "SuperAdmin,SchoolAdmin")]

        [HttpGet("Get-allUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserQueryParameters parameters)
        {
            var result = await _userService.GetAllUsersAsync(parameters);
            return Ok(result);
        }
       
        //[Authorize(Roles = "SuperAdmin")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }
        //[Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var success = await _userService.DeleteUserByIdAsync(id);
            if (!success)
                return NotFound(new { message = "User not found or could not be deleted." });

            return Ok(new { message = "User deleted successfully." });
        }

    }
}
