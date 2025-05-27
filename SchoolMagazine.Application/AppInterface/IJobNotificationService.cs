using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities.JobEntities;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IJobNotificationService
    {
        Task<bool> SubscribeAsync(JobNotificationSubscriptionDto subscriptionDto);
        Task<bool> UpdateSubscriptionAsync(JobNotificationSubscriptionDto subscriptionDto);
        Task<bool> UnsubscribeAsync(Guid userId);
        Task<bool> IsSubscribedAsync(Guid userId);
        Task<List<JobNotificationSubscriptionDto>> GetAllSubscribedUsersAsync();
        Task<JobNotificationSubscriptionDto?> GetUserPreferenceAsync(Guid userId);
        Task NotifySubscribersAsync(JobPostNotificationDto jobPostDto);

    }
}
