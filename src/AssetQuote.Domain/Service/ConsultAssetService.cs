using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Entities.Enuns;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Domain.Service.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service
{
    public class ConsultAssetService : BaseAssetService, IConsultAssetService
    {
        private readonly IBotThreadRepository _botRepository;
        public ConsultAssetService(IBotThreadRepository bot) : base(bot)
        {
            _botRepository = bot;
        }

        public async Task<string> ConsultAsset(BotThread thread)
        {
            await UpdateStep(thread, BotStep.Start);

            thread = await _botRepository.GetBotThreadByChatId(thread.ChatId);

            return await GenerateAssetList(thread.Assets);
        }

        private async Task<string> GenerateAssetList(IEnumerable<Asset> assets) =>
            await Task.FromResult(
                assets.Any() ? 
                    string.Join("\n\n", assets.Select(p => string.Format( $"Ativo: {p.Code} Valor: R$ {p.Valor} \nPorcentagem: {p.Porcentagem}% Valor Ocilação: R$ {p.ValorOcilacao}"))) : 
                "Para consultar é necessário cadastrar um ativo antes.");
    }
}
