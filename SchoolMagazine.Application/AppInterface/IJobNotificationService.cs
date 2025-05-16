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
        Task<bool> SubscribeAsync(Guid userId);
        Task<bool> UnsubscribeAsync(Guid userId);
        Task<bool> IsSubscribedAsync(Guid userId);
       // Task<List<Guid>> GetAllSubscribedUserIdsAsync();
        Task<List<JobNotificationSubscriptionDto>> GetAllSubscribedUsersAsync();
        // Task NotifySubscribersAsync(JobPost jobPost);
      //  Task NotifySubscribersAsync(JobPost jobDto);
        Task NotifySubscribersAsync(JobPostNotificationDto jobPostDto);

    }
}
