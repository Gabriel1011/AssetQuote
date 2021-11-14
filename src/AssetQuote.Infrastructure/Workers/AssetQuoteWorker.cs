namespace AssetQuote.Infrastructure.Workers
{
    public class AssetQuoteWorker : BaseWorker
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
                    if (OpenMarket.CheckOpenMarket())
                    {
                        await Task.Run(async () =>
                        {
                            await scopedProcessingService.UpdateQuote();
                        }, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    SentrySdk.CaptureException(ex);
                }

                await DelayMinutes(Convert.ToDouble(_configuration["WorkerTime:AssetQuote"]), stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
