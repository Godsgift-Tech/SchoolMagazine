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

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("school/{schoolName}")]
        public async Task<IActionResult> GetEventsBySchool(string schoolName)
        {
            var events = await _eventService.GetEventsBySchool(schoolName);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] SchoolEventDto eventDto)
        {
            var response = await _eventService.AddSchoolEventsAsync(eventDto);
            if (!response.Success)
                return BadRequest(response.Message);

            return CreatedAtAction(nameof(GetEventsBySchool), new { schoolName = eventDto.School }, response.Data);
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
    }

}
