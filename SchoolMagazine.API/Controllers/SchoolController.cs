using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Paging;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _ser;

        public SchoolController(ISchoolService ser)
        {
            _ser = ser;
        }


        [HttpGet("get-school-by-ID")]
        public async Task<IActionResult> GetSchoolByIdAsync(Guid id)
        {
            var getSchool = await _ser.GetSchoolByIdAsync(id);
            if (getSchool == null)
            {
                return NotFound(new { Message = "School not found" });

            }
            return Ok(getSchool);
        }

        [HttpGet("get-school-by-SchoolName")]
        public async Task<IActionResult> GetSchoolByName(string schoolName)
        {
            if (string.IsNullOrWhiteSpace(schoolName))
                return BadRequest("School name cannot be empty.");

            var response = await _ser.GetSchoolByNameAsync(schoolName);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }

        [HttpGet("get-schools-by-location")]
        public async Task<IActionResult> GetSchoolsByLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("Location cannot be empty.");

            var response = await _ser.GetSchoolsByLocationAsync(location);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }


        

        [HttpGet("get-schools-by-fees")]
        public async Task<IActionResult> GetSchoolsByFeesRange(decimal feesRange)
        {
            if (feesRange < 0)
                return BadRequest("Fees range cannot be negative.");

            var response = await _ser.GetSchoolsByFeesRangeAsync(feesRange);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }

        [HttpGet("get-schools-by-rating")]
        public async Task<IActionResult> GetSchoolsByRating(double rating)
        {
            if (rating < 0 || rating > 1000)
                return BadRequest("Rating must be between 0 and 1000.");

            var response = await _ser.GetSchoolsByRatingAsync(rating);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }


        [HttpPost("addNewSchool")]
        public async Task<IActionResult> AddSchoolAsync(CreateSchoolDto school)
        {

            var addSchool = await _ser.AddSchoolAsync(school);
            // return Ok(addSchool);

            if (addSchool.success)
            {
                return Ok(addSchool);

            }
            return BadRequest(ModelState);
        }

        [HttpGet("getAllSchools-Adverts")]
        public async Task<IActionResult> GetPagedSchools([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _ser.GetPagedSchoolsAsync(pageNumber, pageSize);

            if (!response.success)
            {
                return BadRequest(response.message);
            }

            return Ok(response);
        }

        [HttpGet("getBysearchQuery")]
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





        [HttpPut("update-school-By-Id")]
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

        public async Task<IActionResult> DeleteSchoolByIdAsync(Guid id)

        {
            var getSchool = await _ser.DeleteSchoolByIdAsync(id);

            return Ok(Ok(getSchool));


        }





    }

}

