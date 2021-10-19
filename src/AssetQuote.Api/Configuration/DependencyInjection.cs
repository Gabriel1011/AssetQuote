using AssetQuote.Domain.Interfaces;
using AssetQuote.Domain.Service;
using AssetQuote.Domain.Workers;
using AssetQuote.Infrastructure.Data;
using AssetQuote.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AssetQuote.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAssetRepository, AssetRepository>();

            services.AddScoped<IAssetService, AssetService>();

            services.AddDbContext<AssetContext>();

            services.AddHostedService<AssetQuoteWorker>();
        }
    }
}
