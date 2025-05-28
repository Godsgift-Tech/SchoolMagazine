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

        public JobNotificationService(
            IJobNotificationRepository jR,
            IAppUserRepository uR,
            IEmailService eS,
            IMapper mapper)
        {
            _jR = jR;
            _uR = uR;
            _eS = eS;
            _mapper = mapper;
        }

        public async Task<bool> SubscribeAsync(JobNotificationSubscriptionDto dto)
        {
            var entity = _mapper.Map<JobNotificationSubscription>(dto);
            return await _jR.SubscribeAsync(entity);
        }

        public async Task<bool> UpdateSubscriptionAsync(JobNotificationSubscriptionDto dto)
        {
            var entity = _mapper.Map<JobNotificationSubscription>(dto);
            return await _jR.UpdateSubscriptionAsync(entity);
        }

        public async Task<bool> UnsubscribeAsync(Guid userId)
        {
            return await _jR.UnsubscribeAsync(userId);
        }

        public async Task<bool> IsSubscribedAsync(Guid userId)
        {
            return await _jR.IsSubscribedAsync(userId);
        }

        public async Task<JobNotificationSubscriptionDto?> GetUserPreferenceAsync(Guid userId)
        {
            var entity = await _jR.GetUserPreferenceAsync(userId);
            if (entity == null) return null;

            return _mapper.Map<JobNotificationSubscriptionDto>(entity);
        }

        public async Task<List<JobNotificationSubscriptionDto>> GetAllSubscribedUsersAsync()
        {
            var subscriptions = await _jR.GetAllSubscribedUsersAsync();

            var filtered = subscriptions
                .Where(x => x.Users != null && !string.IsNullOrWhiteSpace(x.Users.Email))
                .ToList();

            return _mapper.Map<List<JobNotificationSubscriptionDto>>(filtered);
        }

        public async Task NotifySubscribersAsync(JobPostNotificationDto jobPostDto)
        {
            var subscriptions = await _jR.GetAllSubscribedUsersAsync();

            var matched = subscriptions.Where(sub =>
                sub.Categories.Any(c => jobPostDto.Categories
                    .Contains(c.CategoryName, StringComparer.OrdinalIgnoreCase)) &&
                jobPostDto.MinSalary >= sub.MinSalary &&
                jobPostDto.MaxSalary <= sub.MaxSalary &&
                jobPostDto.JobType.Equals(sub.JobType, StringComparison.OrdinalIgnoreCase) &&
                jobPostDto.ExperienceLevel.Equals(sub.ExperienceLevel, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            foreach (var sub in matched)
            {
                var user = sub.Users;
                if (user == null || string.IsNullOrWhiteSpace(user.Email))
                    continue;

                var subject = $"📢 New Job in Your Category: {jobPostDto.Title}";

                // Convert categories list to a string
                var categoriesString = string.Join(", ", jobPostDto.Categories);

                var htmlBody = await _eS.GetJobAlertTemplate(
                    "JobAlertTemplate.html",
                    jobPostDto.Title,
                    jobPostDto.Location,
                    jobPostDto.Qualification,
                    categoriesString,
                    jobPostDto.MinSalary.ToString("N0"),  // formatted as number string
                    jobPostDto.MaxSalary.ToString("N0"),
                    jobPostDto.Description,
                    jobPostDto.PostedAt.ToLocalTime().ToString("f")
                );

                await _eS.SendJobAlertEmailAsync(user.Email, subject, htmlBody);
            }
        }

    }
}

