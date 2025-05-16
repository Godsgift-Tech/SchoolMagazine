using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities.JobEntities;
using SchoolMagazine.Domain.RepositoryInterface;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    public class JobNotificationRepository : IJobNotificationRepository
    {
        private readonly MagazineContext _db;

        public JobNotificationRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task<List<JobNotificationSubscription>> GetAllSubscribedUsersAsync()
        {
            var data = await _db.JobAlerts
                .Include(j => j.Users) // Eagerly load User (navigation property)
                .ToListAsync();

            return data;
        }


        //public async Task<List<Guid>> GetAllSubscribedUserIdsAsync()
        //{
        //    return await _db.JobAlerts.Select(x => x.UserId).ToListAsync();
        //}
        public async Task<List<Guid>> GetAllSubscribedUserIdsAsync()
        {
            return await _db.JobAlerts
                            .Select(alert => alert.UserId)
                            .Distinct()
                            .ToListAsync();
        }
        public async Task<bool> IsSubscribedAsync(Guid userId)
        {
            return await _db.JobAlerts.AnyAsync(x => x.UserId == userId);
        }

        public async Task<bool> SubscribeAsync(Guid userId)
        {
            var alreadySubscribed = await _db.JobAlerts.AnyAsync(x => x.UserId == userId);
            if (alreadySubscribed) return false;

            _db.JobAlerts.Add(new JobNotificationSubscription { UserId = userId });
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnsubscribeAsync(Guid userId)
        {
            var sub = await _db.JobAlerts.FirstOrDefaultAsync(x => x.UserId == userId);
            if (sub == null) return false;

            _db.JobAlerts.Remove(sub);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
