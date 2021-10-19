using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Workers
{
    public class AssetQuoteWorker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    Console.WriteLine("Teste");
                }, stoppingToken);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            await Task.CompletedTask;
        }
    }
}
