using AssetQuote.Domain.Helprs;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.Workers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Workers
{
    public class AssetQuoteMessageWorker : BaseWorker
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
                var scope = _serviceProvider.CreateScope();

                IBotThreadRepository scopedProcessingService = scope.ServiceProvider.GetRequiredService<IBotThreadRepository>();
                IBotMessage botMessage = scope.ServiceProvider.GetRequiredService<IBotMessage>();
                IConsultAssetService consultAssetService = scope.ServiceProvider.GetRequiredService<IConsultAssetService>();

                var chats = await scopedProcessingService.All();

                if (OpenMarket.CheckOpenMarket())
                {
                    await Task.Run(async () =>
                    {
                        foreach (var chat in chats)
                        {
                            var asses = await consultAssetService.ConsultAsset(chat);
                            await botMessage.SendMessage(chat.ChatId, asses);
                        }
                    }, stoppingToken);
                }

                await DelayHours(1, stoppingToken);
            }
        }
    }
}
