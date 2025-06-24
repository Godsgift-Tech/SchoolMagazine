using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;

namespace SchoolMagazine.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SchoolAdmin")]
    public class SchoolDashboardController : ControllerBase
    {
        private readonly ISchoolDashboardService _dashboardService;

        public SchoolDashboardController(ISchoolDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get dashboard data for a specific school with optional date range filtering.
        /// </summary>
        /// <param name="schoolId">The ID of the school</param>
        /// <param name="fromDate">Optional start date</param>
        /// <param name="toDate">Optional end date</param>
        /// <returns>A dashboard summary including events, adverts, job posts, and applications</returns>
        [HttpGet("{schoolId}")]
        public async Task<IActionResult> GetDashboard(Guid schoolId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var dashboard = await _dashboardService.GetDashboardDataAsync(schoolId, fromDate, toDate);
            return Ok(dashboard);
        }
    }
}
