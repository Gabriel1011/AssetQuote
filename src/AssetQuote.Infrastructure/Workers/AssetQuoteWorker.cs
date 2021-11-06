using AssetQuote.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sentry;
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
                try
                {
                    await Task.Run(async () =>
                    {
                        await scopedProcessingService.UpdateQuote();
                    }, stoppingToken);
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureException(ex);
                }

                Thread.Sleep(TimeSpan.FromMinutes(Convert.ToDouble(_configuration["WorkerTime:AssetQuote"])));
            }

            await Task.CompletedTask;
        }

        private async Task<bool> CheckOpenIBOV()
        {
            return await Task.Run(() =>
            {
                var now = DateTime.Now;
                return Convert.ToInt32(_configuration["Quote:Start"]) > now.Hour && now.Hour < Convert.ToInt32(_configuration["Quote:end"]);
            });
        }
    }
}
