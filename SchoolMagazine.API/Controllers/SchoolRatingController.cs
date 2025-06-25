using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolRatingController : ControllerBase
    {
        private readonly ISchoolRatingService _ratingService;

        public SchoolRatingController(ISchoolRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateSchool([FromBody] RateSchoolDto dto)
        {
            await _ratingService.AddRatingAsync(dto);
            return Ok("Rating submitted successfully.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        [HttpGet("school/{schoolName}")]
        public async Task<IActionResult> GetRatingsForSchool(string schoolName)
        {
            var ratings = await _ratingService.GetRatingsForSchoolAsync(schoolName);
            return Ok(ratings);
        }

        [HttpGet("by-average")]
        public async Task<IActionResult> GetSchoolsByMinAverageRating([FromQuery] double minRating)
        {
            var schools = await _ratingService.GetSchoolsByAverageRatingAsync(minRating);
            return Ok(schools);
        }
    }
}
