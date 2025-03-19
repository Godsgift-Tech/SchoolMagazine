using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {


        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

      
        [HttpPost("createAdvert")]

        public async Task<IActionResult> CreateAdvert([FromBody] CreateAdvertDto advertDto)
        {
            var response = await _advertService.CreateAdvertAsync(advertDto);
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("allAdverts")]
        public async Task<IActionResult> GetAllAdverts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _advertService.GetAllAdvertsAsync(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("{advertId}")]
        public async Task<IActionResult> GetAdvertById(Guid advertId)
        {
            var response = await _advertService.GetAdvertByIdAsync(advertId);
            if (!response.success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetAdvertsBySchoolId(Guid schoolId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _advertService.GetAdvertsBySchoolIdAsync(schoolId, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("BysearchQuery")]
        public async Task<IActionResult> SearchAdverts([FromQuery] string keyword, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _advertService.SearchAdvertsAsync(keyword, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpDelete("{advertId}")]
        public async Task<IActionResult> DeleteAdvert(Guid advertId)
        {
            var response = await _advertService.DeleteAdvertAsync(advertId);
            if (!response.success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
