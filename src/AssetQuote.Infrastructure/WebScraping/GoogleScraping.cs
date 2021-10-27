using AssetQuote.Domain.Entities;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.WebScraping
{
    public class GoogleScraping
    {
        public async Task<Asset> ReadPage(string assetCode)
        {

            await Task.Run(() => {
                var conexao = new WebClient();
                var link = $@"https://www.google.com/finance/quote/{assetCode}:BVMF";
                var pag = conexao.DownloadString(link);

                var indx = pag.Split(new char[] { '<', '>' });
                var valores = indx.FirstOrDefault(p => p.Contains("\"BRL\",[")).Split(',')[8..11];

                var valor = valores[0].Replace("[", "");
                var ValorOcilacao = valores[1];
                var porcentagem = valores[2];

                Console.WriteLine(String.Join(',', valores));
            });

            return await Task.FromResult(new Asset());
        }
    }
}
