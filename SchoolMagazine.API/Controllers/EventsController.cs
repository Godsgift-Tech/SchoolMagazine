using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        

        [HttpGet("GetAllEvents")]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> GetAllEvents(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            var result = await _eventService.GetAllEventsAsync(pageNumber, pageSize);
            return result.success ? Ok(result) : NotFound(result);
        }


        [HttpPost("createEvent")]
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]

        public async Task<IActionResult> AddEvent([FromBody] CreateEventDto eventDto)
        {
            if (eventDto == null)
            {
                return BadRequest("Event details are required.");
            }

            var response = await _eventService.AddSchoolEventsAsync(eventDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }


        [HttpPut("updateEvent/{id}")]
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]

        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] SchoolEventDto eventDto)
        {
            var response = await _eventService.UpdateSchoolEventAsync(id, eventDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteSchoolEventAsync(id);
            return Ok("Event deleted successfully.");
        }


        [HttpGet("EventS-BysearchQuery")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvents(
    [FromQuery] string? title,
    [FromQuery] string? description,
    [FromQuery] Guid? schoolId,
    [FromQuery] string? schoolName,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            var result = await _eventService.GetEventsAsync(title, description, schoolId, schoolName, pageNumber, pageSize);
            return result.success ? Ok(result) : NotFound(result);
        }

    }

}
