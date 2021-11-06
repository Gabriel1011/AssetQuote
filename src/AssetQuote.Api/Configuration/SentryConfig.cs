using Microsoft.AspNetCore.Builder;

namespace AssetQuote.Api.Configuration
{
    public static class SentryConfig
    {
        public static void UseSentry(this IApplicationBuilder app)
        {
            app.UseRouting();

            // Enable automatic tracing integration.
            // Make sure to put this middleware right after `UseRouting()`.
            app.UseSentryTracing();
        }
    }
}
