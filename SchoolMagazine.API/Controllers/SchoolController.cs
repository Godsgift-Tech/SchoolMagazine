using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
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
        [HttpPost("Add a new School")]
        public async Task <IActionResult> AddSchoolAsync(SchoolDto school)
        {
            var addSchool = await _ser.AddSchoolAsync(school);  
            return Ok(addSchool);
        }
    }
}
