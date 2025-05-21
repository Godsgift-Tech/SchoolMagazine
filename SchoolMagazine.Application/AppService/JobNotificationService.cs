using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities.JobEntities;
using SchoolMagazine.Domain.RepositoryInterface;

namespace SchoolMagazine.Application.AppService
{
    public class JobNotificationService : IJobNotificationService
    {
        private readonly IJobNotificationRepository _jR;
        private readonly IAppUserRepository _uR;
        private readonly IEmailService _eS;
        private readonly IMapper _mapper;

        public JobNotificationService(IJobNotificationRepository jR, IAppUserRepository uR, IEmailService eS, IMapper mapper)
        {
            _jR = jR;
            _uR = uR;
            _eS = eS;
            _mapper = mapper;
        }



        public async Task<List<JobNotificationSubscriptionDto>> GetAllSubscribedUsersAsync()
        {
            // Fetch subscribed users with navigation property already included
            var subscriptions = await _jR.GetAllSubscribedUsersAsync();

            // Filter out those without user or email, then map to DTOs using AutoMapper
            var filteredSubscriptions = subscriptions
                .Where(x => x.Users != null && !string.IsNullOrWhiteSpace(x.Users.Email))
                .ToList();

            // Map the filtered list to DTOs
            var result = _mapper.Map<List<JobNotificationSubscriptionDto>>(filteredSubscriptions);

            return result;
        }


        public async Task<bool> IsSubscribedAsync(Guid userId) => await _jR.IsSubscribedAsync(userId);
        public async Task NotifySubscribersAsync(JobPostNotificationDto jobPostDto)
        {
            // Step 1: Get all subscribed user IDs
            var subscribedUserIds = await _jR.GetAllSubscribedUserIdsAsync();

            // Step 2: Fetch user details by IDs
            var users = await _uR.GetUsersByIdsAsync(subscribedUserIds);

            // Step 3: Send job alert email to each user
            foreach (var user in users)
            {
                if (string.IsNullOrWhiteSpace(user.Email))
                    continue;

                var subject = $"📢 New Job Opening: {jobPostDto.Title}";

                var htmlBody = await _eS.GetJobAlertTemplate(
                    "JobAlertTemplate.html",
                    jobPostDto.Title,
                    jobPostDto.Location,
                    jobPostDto.Qualification,
                    jobPostDto.Description,
                    jobPostDto.PostedAt.ToLocalTime().ToString("f")
                );

                await _eS.SendJobAlertEmailAsync(user.Email, subject, htmlBody);
            }
        }
       

        public async Task<bool> SubscribeAsync(Guid userId) => await _jR.SubscribeAsync(userId);
       

        public async Task<bool> UnsubscribeAsync(Guid userId) => await _jR.UnsubscribeAsync(userId);
       
    }
}
