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

        // Subscribe current user to job notifications
        [HttpPost("subscribe/{userId}")]
        public async Task<IActionResult> Subscribe(Guid userId)
        {
            var result = await _jobNotificationService.SubscribeAsync(userId);
            if (!result) return BadRequest("Subscription failed or user already subscribed.");
            return Ok("Subscribed successfully.");
        }

        // Unsubscribe current user
        [HttpPost("unsubscribe/{userId}")]
        public async Task<IActionResult> Unsubscribe(Guid userId)
        {
            var result = await _jobNotificationService.UnsubscribeAsync(userId);
            if (!result) return BadRequest("Unsubscription failed or user was not subscribed.");
            return Ok("Unsubscribed successfully.");
        }

        // Check if user is subscribed
        [HttpGet("issubscribed/{userId}")]
        public async Task<IActionResult> IsSubscribed(Guid userId)
        {
            var subscribed = await _jobNotificationService.IsSubscribedAsync(userId);
            return Ok(new { UserId = userId, IsSubscribed = subscribed });
        }

        // Endpoint to notify subscribers about a new job (for admins or job posters)
        [HttpPost("notify")]
        public async Task<IActionResult> NotifySubscribers([FromBody] JobPostNotificationDto jobPost)
        {
            if (jobPost == null) return BadRequest("Job post data is required.");

            await _jobNotificationService.NotifySubscribersAsync(jobPost);
            return Ok("Notifications sent to all subscribers.");
        }
    }
}
