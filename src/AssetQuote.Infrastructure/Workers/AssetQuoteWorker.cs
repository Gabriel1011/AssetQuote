using AssetQuote.Infrastructure.WebScraping;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Workers
{
    public class AssetQuoteWorker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scraping =  new GoogleScraping();

                await scraping.ReadPage("OIBR3");
                await scraping.ReadPage("PETR4");
                await scraping.ReadPage("MXRF11");
                await scraping.ReadPage("HGBS11 ");

                //await Task.Run(() =>
                //{
                //    new GoogleScraping().ReadPage("");
                //}, stoppingToken);

                Thread.Sleep(TimeSpan.FromMinutes(5));
            }

            await Task.CompletedTask;
        }
    }
}
