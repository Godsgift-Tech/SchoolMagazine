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
            return await _db.JobAlerts
                .Include(j => j.Users)
                .Include(j => j.Categories)
                .ToListAsync();
        }

        public async Task<List<Guid>> GetAllSubscribedUserIdsAsync()
        {
            return await _db.JobAlerts
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> IsSubscribedAsync(Guid userId)
        {
            return await _db.JobAlerts.AnyAsync(x => x.UserId == userId);
        }

        public async Task<bool> SubscribeAsync(JobNotificationSubscription subscription)
        {
            var exists = await _db.JobAlerts.AnyAsync(x => x.UserId == subscription.UserId);
            if (exists) return false;

            _db.JobAlerts.Add(subscription);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSubscriptionAsync(JobNotificationSubscription updatedSubscription)
        {
            var existing = await _db.JobAlerts
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.UserId == updatedSubscription.UserId);

            if (existing == null) return false;

            // Update scalar fields
            existing.JobType = updatedSubscription.JobType;
            existing.MinSalary = updatedSubscription.MinSalary;
            existing.MaxSalary = updatedSubscription.MaxSalary;
            existing.ExperienceLevel = updatedSubscription.ExperienceLevel;
            existing.NotificationFrequency = updatedSubscription.NotificationFrequency;
            existing.SubscribedAt = DateTime.UtcNow;

            // Remove old category preferences
            _db.JobCategoryPreferences.RemoveRange(existing.Categories);

            // Add new category preferences
            existing.Categories = updatedSubscription.Categories.Select(c => new JobCategoryPreference
            {
                CategoryName = c.CategoryName,
                SubscriptionId = existing.Id
            }).ToList();

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnsubscribeAsync(Guid userId)
        {
            var sub = await _db.JobAlerts
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (sub == null) return false;

            _db.JobCategoryPreferences.RemoveRange(sub.Categories);
            _db.JobAlerts.Remove(sub);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<JobNotificationSubscription?> GetUserPreferenceAsync(Guid userId)
        {
            return await _db.JobAlerts
                .Include(j => j.Categories)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
