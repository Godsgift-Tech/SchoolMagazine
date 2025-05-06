using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Application.AppService.BackgroudApp;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation.Tracker
{
    public class SubscriptionTrackerService : ISubscriptionTrackerService
    {
        private readonly MagazineContext _context;

        public SubscriptionTrackerService(MagazineContext context)
        {
            _context = context;
        }

        public async Task TrackExpiredVendorsAsync()
        {
            var expiredVendors = await _context.Vendors
                .Where(v => v.SubscriptionEndDate < DateTime.UtcNow && v.HasActiveSubscription)
                .ToListAsync();

            foreach (var vendor in expiredVendors)
            {
                vendor.HasActiveSubscription = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}
