using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Sentry;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.WebScraping
{
    public class StatusInvestingScraping : IWebScraping
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
                var page = await GetPage(asset.Code);
                var valores = await GetValuesAsset(page);

                try
                {
                    asset.Porcentagem = await ConvertValue(valores[1]);
                    asset.Valor = await ConvertValue(valores[0].Remove(0, 1));
                    asset.ValorOcilacao = await ConvertValue(valores[2]);
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

        private async Task<string> GetPage(string code)
        {
            var link = string.Format(_configuration["WebScraping:StatusInvestUrl"], code);
            return await Task.FromResult(new WebClient().DownloadString(link));
        }

        private async Task<string[]> GetValuesAsset(string page) => 
            await Task.FromResult(page.Split(new char[] { '<', '>' })
                .FirstOrDefault(p => p.Contains("\"BRL\",["))
                .Split(',')[8..11]);
        
        public async Task UpdateQuote()
        {
            var assets = await _assetRepository.All();

            await Task.Run(async () =>
            {
                foreach (var asset in assets)
                    await _assetRepository.Update(await ReadPage(asset));
            });
        }
    }
}
