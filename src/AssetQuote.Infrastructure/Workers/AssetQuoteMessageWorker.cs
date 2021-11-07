using AssetQuote.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Workers
{
    public class AssetQuoteMessageWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AssetQuoteMessageWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Now.Hour > 9 && DateTime.Now.Hour < 18)
                {
                    var scope = _serviceProvider.CreateScope();
                    IBotMessage botMessage = scope.ServiceProvider.GetRequiredService<IBotMessage>();

                    await botMessage.GenerateMessage();

                    Thread.Sleep(TimeSpan.FromHours(1));
                }

                await Task.Run(() => Thread.Sleep(TimeSpan.FromHours(1)));
            }
        }
    }
}
