﻿using AssetQuote.Domain.Interfaces.Repositories;
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
                var scope = _serviceProvider.CreateScope();

                IBotThreadRepository scopedProcessingService = scope.ServiceProvider.GetRequiredService<IBotThreadRepository>();
                IBotMessage botMessage = scope.ServiceProvider.GetRequiredService<IBotMessage>();
                IConsultAssetService consultAssetService = scope.ServiceProvider.GetRequiredService<IConsultAssetService>();

                var chats = await scopedProcessingService.All();

                await Task.Run(async () =>
                {
                    foreach (var chat in chats)
                    {
                        var asses = await consultAssetService.ConsultAsset(chat);
                        await botMessage.SendMessage(chat.ChatId, asses);
                    }
                });

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }       
        }
    }
}