using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.Entities.JobEntities;

namespace SchoolMagazine.Domain.RepositoryInterface
{
    public interface IJobNotificationRepository
    {
        Task<bool> SubscribeAsync(Guid userId);
        Task<bool> UnsubscribeAsync(Guid userId);
        Task<bool> IsSubscribedAsync(Guid userId);
        Task<List<Guid>> GetAllSubscribedUserIdsAsync();
        Task<List<JobNotificationSubscription>> GetAllSubscribedUsersAsync();
    }
}
