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

        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        //[HttpGet("GetEventsBySchool/{schoolName}")]
        //public async Task<IActionResult> GetEventsBySchool(string schoolName)
        //{
        //    var events = await _eventService.GetEventsBySchoolAsync(schoolName);
        //    return Ok(events);
        //}
        //[HttpGet("by-school/{schoolId}")]
        //public async Task<IActionResult> GetEventsBySchool(Guid schoolId)
        //{
        //    var events = await _eventService.GetEventsBySchool(schoolId);
        //    return Ok(events);
        //}


        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] SchoolEventDto eventDto)
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


        [HttpPut("{id}")]
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
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteSchoolEventAsync(id);
            return Ok("Event deleted successfully.");
        }


        [HttpGet("search")]
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
