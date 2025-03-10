using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService;
using SchoolMagazine.Application.AppService.Paged;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;

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


        [HttpGet("getAllSchools")]

        public async Task<IActionResult> GetAllSchoolAsync()
        {
            var allschool = await _ser.GetAllSchoolAsync();
            return Ok(allschool);
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
            if (rating < 0 || rating > 5)
                return BadRequest("Rating must be between 0 and 5.");

            var response = await _ser.GetSchoolsByRatingAsync(rating);

            if (!response.success)
                return NotFound(response.message);

            return Ok(response);
        }


        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<SchoolDto>>> GetPagedSchoolAsync(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10)
        {
            var result = await _ser.GetPagedSchoolAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("addNewSchool")]
        public async Task<IActionResult> AddSchoolAsync(SchoolDto school)
        {

            var addSchool = await _ser.AddSchoolAsync(school);
            return Ok(addSchool);

            //if (addSchool.success)
            //{
            //    return Ok(addSchool);

            //}
            //return BadRequest(ModelState);
        }






        //[HttpPut("updateSchool")]
        //public async Task<IActionResult> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var updateSchool = await _ser.GetSchoolByIdAsync(id);
        //    if (updateSchool == null)
        //    {
        //        return NotFound(new { Message = "School not found" });

        //    }
        //    try
        //    {

        //        await _ser.UpdateSchoolByIdAsync(id, schoolDto);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound(new { Message = "School not found" });
        //    }

        //}

        [HttpPut("update-school-By-Id")]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] SchoolDto schoolDto)
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

