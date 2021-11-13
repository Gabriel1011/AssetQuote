using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AssetQuote.Infrastructure.Workers.Base
{
    public abstract class BaseWorker : BackgroundService
    {
        protected async Task DelayHours(double time, CancellationToken stoppingToken = new()) =>
            await Task.Delay(TimeSpan.FromHours(time), stoppingToken);

        protected async Task DelayMinutes(double time, CancellationToken stoppingToken = new()) =>
            await Task.Delay(TimeSpan.FromMinutes(time), stoppingToken);
    }
}
