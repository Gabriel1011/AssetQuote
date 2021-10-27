using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.WebScraping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Workers
{
    public class AssetQuoteWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AssetQuoteWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _serviceProvider.CreateScope();

            GoogleScraping scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<GoogleScraping>();

            while (!stoppingToken.IsCancellationRequested)
            {
               await Task.Run(async () =>
               {
                   await scopedProcessingService.UpdateQuote();
               }, stoppingToken);

                Thread.Sleep(TimeSpan.FromMinutes(5));
            }

            await Task.CompletedTask;
        }
    }
}
