using AssetQuote.Domain.Interfaces.ExternalService;

namespace AssetQuote.Infrastructure.WebScraping;
public class StatusInvestingScraping : BaseWebScraping, IWebScraping
{
    private readonly IAssetRepository _assetRepository;
    private readonly IConfiguration _configuration;

    public StatusInvestingScraping(IAssetRepository assetRepository, IConfiguration configuration)
    {
        _assetRepository = assetRepository;
        _configuration = configuration;
    }

    private async Task<Asset> ReadPage(Asset asset)
    {
        await Task.Run(async () =>
        {
            var page = await GetPage(asset.Code, _configuration["WebScraping:StatusInvestUrl"]);
            var valores = await GetValuesAsset(page);

            try
            {
                asset.Percentage = await ConvertValue(valores[1]);
                asset.Value = await ConvertValue(valores[0].Remove(0, 1));
                asset.OcillationValue = await ConvertValue(valores[2]);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                SentrySdk.CaptureMessage("Valores obtidos da página do Google" + string.Join(',', valores), SentryLevel.Error);
            }

        });

        async Task<double> ConvertValue(string value) => await Task.FromResult(Math.Round(Convert.ToDouble(value.Replace('.', ',')), 2));

        return await Task.FromResult(asset);
    }

    private async Task<string[]> GetValuesAsset(string page) =>
        await Task.FromResult(page.Split(new char[] { '<', '>' })
            .FirstOrDefault(p => p.Contains("\"BRL\",["))
            .Split(',')[8..11]);

    public async Task UpdateAllQuote()
    {
        var assets = await _assetRepository.All();

        await Task.Run(async () =>
        {
            foreach (var asset in assets)
                await UpdateQuote(asset);
        });
    }

    public async Task UpdateQuote(Asset asset)
    {
        var assetUpdated = await ReadPage(asset);

        try
        {
            await _assetRepository.Update(assetUpdated);
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
            SentrySdk.CaptureMessage("Valores obtidos da página do Google" + string.Join(',', assetUpdated), SentryLevel.Error);
        }
    }
}
