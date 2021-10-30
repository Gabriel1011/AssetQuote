using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.WebScraping
{
    public class GoogleScraping : IWebScraping
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
                var link = string.Format(_configuration["WebScraping:GoogleUrl"], asset.Code);
                var pag = new WebClient().DownloadString(link);

                var valores = pag.Split(new char[] { '<', '>' })
                .FirstOrDefault(p => p.Contains("\"BRL\",["))
                .Split(',')[8..11];

                asset.Valor = await ConvertValue(valores[0].Remove(0, 1));
                asset.Porcentagem = await ConvertValue(valores[1]);
                asset.ValorOcilacao = await ConvertValue(valores[2]);

            });

            async Task<double> ConvertValue(string value) => await Task.FromResult(Math.Round(Convert.ToDouble(value.Replace('.', ',')), 2));

            return await Task.FromResult(asset);
        }

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
