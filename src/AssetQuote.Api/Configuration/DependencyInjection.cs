using AssetQuote.Domain.Workers;
using AssetQuote.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AssetQuote.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddDbContext<AssetContext>();
            services.AddHostedService<AssetQuoteWorker>();
        }
    }
}
