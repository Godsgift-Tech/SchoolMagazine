using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Paging;
using System.Security.Claims;

namespace SchoolMagazine.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]

    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _ser;

        public SchoolController(ISchoolService ser)
        {
            _ser = ser;
        }


        [HttpGet("getSchoolBy-ID/{id}")]
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]

        public async Task<IActionResult> GetSchoolByIdAsync(Guid id)
        {
            var getSchool = await _ser.GetSchoolByIdAsync(id);
            if (getSchool == null)
            {
                return NotFound(new { Message = "School not found" });

            }
            return Ok(getSchool);
        }

        [HttpGet("by-SchoolName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSchoolByName(string schoolName)
        {
            if (string.IsNullOrWhiteSpace(schoolName))
                return BadRequest("School name cannot be empty.");

            var response = await _ser.GetSchoolByNameAsync(schoolName);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }




        [HttpPost("addNewSchool")]
       
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task<IActionResult> AddSchoolAsync(CreateSchoolDto school)
        {

            var addSchool = await _ser.AddSchoolAsync(school);

            if (addSchool.success)
            {
                return Ok(addSchool);

            }
            return BadRequest(ModelState);
        }


        [HttpGet("Events-AdvertsDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedSchools([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _ser.GetPagedSchoolsAsync(pageNumber, pageSize);

            if (!response.success)
            {
                return BadRequest(response.message);
            }

            return Ok(response);
        }



        [HttpGet("BysearchQuery")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSchools(
    [FromQuery] string? schoolName,
    [FromQuery] string? location,
    [FromQuery] decimal? feesRange,
    [FromQuery] double? rating,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            var result = await _ser.GetSchoolsAsync(schoolName, location, feesRange, rating, pageNumber, pageSize);
            return result.success ? Ok(result) : NotFound(result);
        }


        [HttpPut("updateSchoolBy-Id/{id}")]
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] CreateSchoolDto schoolDto)
        {
            if (schoolDto == null)
                return BadRequest("Invalid school data.");

            var response = await _ser.UpdateSchoolByIdAsync(id, schoolDto);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> DeleteSchoolByIdAsync(Guid id)

        {
            var getSchool = await _ser.DeleteSchoolByIdAsync(id);

            return Ok(Ok(getSchool));


        }

    }

}

