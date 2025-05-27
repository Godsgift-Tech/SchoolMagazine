using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobNotificationsController : ControllerBase
    {

        private readonly IJobNotificationService _jobNotificationService;

        public JobNotificationsController(IJobNotificationService jobNotificationService)
        {
            _jobNotificationService = jobNotificationService;
        }

        // POST: api/JobNotification/subscribe
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] JobNotificationSubscriptionDto dto)
        {
            var result = await _jobNotificationService.SubscribeAsync(dto);
            if (result)
                return Ok(new { message = "Subscription successful." });
            return BadRequest(new { message = "Subscription failed." });
        }

        // PUT: api/JobNotification/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSubscription([FromBody] JobNotificationSubscriptionDto dto)
        {
            var result = await _jobNotificationService.UpdateSubscriptionAsync(dto);
            if (result)
                return Ok(new { message = "Subscription updated successfully." });
            return BadRequest(new { message = "Update failed." });
        }

        // DELETE: api/JobNotification/unsubscribe/{userId}
        [HttpDelete("unsubscribe/{userId:guid}")]
        public async Task<IActionResult> Unsubscribe(Guid userId)
        {
            var result = await _jobNotificationService.UnsubscribeAsync(userId);
            if (result)
                return Ok(new { message = "Unsubscribed successfully." });
            return NotFound(new { message = "Subscription not found." });
        }

        // GET: api/JobNotification/isSubscribed/{userId}
        [HttpGet("isSubscribed/{userId:guid}")]
        public async Task<IActionResult> IsSubscribed(Guid userId)
        {
            var isSubscribed = await _jobNotificationService.IsSubscribedAsync(userId);
            return Ok(new { userId, isSubscribed });
        }

        // GET: api/JobNotification/preferences/{userId}
        [HttpGet("preferences/{userId:guid}")]
        public async Task<IActionResult> GetUserPreferences(Guid userId)
        {
            var preferences = await _jobNotificationService.GetUserPreferenceAsync(userId);
            if (preferences == null)
                return NotFound(new { message = "User preferences not found." });

            return Ok(preferences);
        }

        // GET: api/JobNotification/subscribers
        [HttpGet("subscribers")]
        public async Task<IActionResult> GetAllSubscribers()
        {
            var subscribers = await _jobNotificationService.GetAllSubscribedUsersAsync();
            return Ok(subscribers);
        }

        // POST: api/JobNotification/notify
        [HttpPost("notify")]
        public async Task<IActionResult> NotifySubscribers([FromBody] JobPostNotificationDto jobPostDto)
        {
            await _jobNotificationService.NotifySubscribersAsync(jobPostDto);
            return Ok(new { message = "Notifications sent to matched subscribers." });
        }
    }
}
