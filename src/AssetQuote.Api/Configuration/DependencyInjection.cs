using AssetQuote.Data.Repositories;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Domain.Service;
using AssetQuote.Infrastructure.Data;
using AssetQuote.Infrastructure.Interfaces;
using AssetQuote.Infrastructure.Telegram;
using AssetQuote.Infrastructure.WebScraping;
using AssetQuote.Infrastructure.Workers;
using AssetQuote.Infrastructure.Workes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetQuote.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IBotService, BotService>();
            services.AddTransient<ICreateAssetService, CreateAssetService>();
            services.AddTransient<IConsultAssetService, ConsultAssetService>();
            services.AddTransient<IRemoveAssetService, RemoveAssetService>();

            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<IBotThreadRepository, BotThreadRepository>();

            services.AddTransient<IBot, TelegramBot>();
            services.AddTransient<IWebScraping, GoogleScraping>();

            services.AddDbContext<AssetContext>(options => 
                options.UseNpgsql(configuration["dbContextSettings:ConnectionString"]));

            services.AddHostedService<AssetQuoteWorker>();
            services.AddHostedService<BotWorker>();

        }
    }
}
