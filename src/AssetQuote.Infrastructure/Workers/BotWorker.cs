using AssetQuote.Domain.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace AssetQuote.Infrastructure.Workes
{
    public class BotWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BotWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            IBot scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IBot>();

            await scopedProcessingService.Send();
        }
    }
}
