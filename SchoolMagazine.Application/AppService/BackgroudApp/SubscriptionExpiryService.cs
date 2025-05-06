using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolMagazine.Application.AppService.BackgroudApp;

namespace SchoolMagazine.Application.AppService.BackgroundApp
{
    public class SubscriptionExpiryService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public SubscriptionExpiryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var trackerService = scope.ServiceProvider.GetRequiredService<ISubscriptionTrackerService>();

                await trackerService.TrackExpiredVendorsAsync();

                await Task.Delay(TimeSpan.FromHours(12), stoppingToken); // Runs every 12 hours
            }
        }
    }
}
