using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService.Paged;
using SchoolMagazine.Application.DTOs;

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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolByIdAsync(Guid id)
        {
            var getSchool = await _ser.GetSchoolByIdAsync(id);
            if (getSchool == null)
            {
                return NotFound(new { Message = "School not found" });

            }
            return Ok(getSchool);
        }

        [HttpGet("getAllSchools")]

        public async Task<IActionResult> GetAllSchoolAsync()
        {
            var allschool = await _ser.GetAllSchoolAsync();
            return Ok(allschool);
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


        }

        [HttpPut("updateSchool")]
        public async Task<IActionResult> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var updateSchool = await _ser.GetSchoolByIdAsync(id);
            if (updateSchool == null)
            {
                return NotFound(new { Message = "School not found" });

            }
            try
            {

                await _ser.UpdateSchoolByIdAsync(id, schoolDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "School not found" });
            }

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSchoolByIdAsync(Guid id)

        {
            var getSchool = await _ser.DeleteSchoolByIdAsync(id);

            return Ok(Ok(getSchool));


        }





    }

}

