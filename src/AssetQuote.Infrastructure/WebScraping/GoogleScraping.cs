using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.WebScraping
{
    public class GoogleScraping
    {
        private readonly IAssetRepository _assetRepository;

        public GoogleScraping(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<Asset> ReadPage(Asset asset)
        {
            await Task.Run(() =>
            {
                var conexao = new WebClient();
                var link = $@"https://www.google.com/finance/quote/{asset.Code}:BVMF";
                var pag = conexao.DownloadString(link);

                var indx = pag.Split(new char[] { '<', '>' });
                var valores = indx.FirstOrDefault(p => p.Contains("\"BRL\",[")).Split(',')[8..11];

                asset.Valor = Convert.ToDouble(valores[0].Replace("[", "").Replace('.', ','));
                asset.Porcentagem = Convert.ToDouble(valores[1].Replace('.', ','));
                asset.ValorOcilacao = Convert.ToDouble(valores[2].Replace('.', ','));
            });

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
