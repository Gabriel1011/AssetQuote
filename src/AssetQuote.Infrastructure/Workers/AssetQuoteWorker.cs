using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.Interfaces;
using AssetQuote.Infrastructure.WebScraping;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public AssetQuoteWorker(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _serviceProvider.CreateScope();

            IWebScraping scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IWebScraping>();

            while (!stoppingToken.IsCancellationRequested)
            {
               await Task.Run(async () =>
               {
                   await scopedProcessingService.UpdateQuote();
               }, stoppingToken);

                Thread.Sleep(TimeSpan.FromMinutes(Convert.ToDouble(_configuration["WorkerTime:AssetQuote"])));
            }

            await Task.CompletedTask;
        }
    }
}
