using AssetQuote.Data.Repositories;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Domain.Service;
using AssetQuote.Infrastructure.Data;
using AssetQuote.Infrastructure.Telegram;
using AssetQuote.Infrastructure.Workers;
using AssetQuote.Infrastructure.Workes;
using Microsoft.Extensions.DependencyInjection;

namespace AssetQuote.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<IBotService, BotService>();
            services.AddTransient<IBot, TelegramBot>();

            services.AddDbContext<AssetContext>();
            services.AddHostedService<AssetQuoteWorker>();
            services.AddHostedService<BotWorker>();
        }
    }
}
