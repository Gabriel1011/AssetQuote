using System.Globalization;

namespace AssetQuote.Infrastructure.WebScraping;
public class GoogleScraping : BaseWebScraping, IWebScraping
{
    private readonly IAssetRepository _assetRepository;
    private readonly IConfiguration _configuration;

    public GoogleScraping(IAssetRepository assetRepository, IConfiguration configuration)
    {
        _assetRepository = assetRepository;
        _configuration = configuration;
    }

    private async Task<Asset> ReadPage(Asset asset)
    {
        await Task.Run(async () =>
        {
            var page = await GetPage(asset.Code, _configuration["WebScraping:GoogleUrl"]);
            var valores = await GetValuesAsset(page);

            try
            {
                asset.Valor = await ConvertValue(valores[0].Remove(0, 1));
                asset.Porcentagem = await ConvertValue(valores[1]);
                asset.ValorOcilacao = await ConvertValue(valores[2]);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                SentrySdk.CaptureMessage("Valores obtidos da página do Google" + string.Join(',', valores), SentryLevel.Error);
            }
        });

        async Task<double> ConvertValue(string value) =>
            await Task.FromResult(Math.Round(Convert.ToDouble(value.Replace(".", ","), new CultureInfo("pt-BR")), 2));

        return await Task.FromResult(asset);
    }
    private async Task<string[]> GetValuesAsset(string page)
    {
        var values = page.Split(new char[] { '<', '>' })
            .FirstOrDefault(p => p.Contains("\"BRL\",["))
            .Split(',');

            return await Task.FromResult(values[10].Contains("BRL") ? values[11..14] : values[8..11]);
    }

    public async Task UpdateQuote()
    {
        var assets = await _assetRepository.All();

        await Task.Run(async () =>
        {
            foreach (var asset in assets)
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
        });
    }
}
