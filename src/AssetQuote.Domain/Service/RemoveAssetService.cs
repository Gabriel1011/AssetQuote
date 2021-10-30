using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Entities.Enuns;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Domain.Service.Base;
using System.Linq;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service
{
    public class RemoveAssetService : BaseAssetService, IRemoveAssetService
    {
        private readonly IBotThreadRepository _botRepository;
        private readonly IAssetRepository _assetRepository;

        public RemoveAssetService(IBotThreadRepository bot, IAssetRepository assetRepository) : base(bot)
        {
            _botRepository = bot;
            _assetRepository = assetRepository;
        }

        public async Task<string> StartDeletation(BotThread botThread)
        {
            await UpdateStep(botThread, BotStep.ConfirmRemoveAsset);

            return await Task.FromResult("Informe o ativo que deseja remover da sua lista.");
        }

        public async Task<string> ConfirmDeletation(BotThread botThread)
        {
            await UpdateStep(botThread, BotStep.Start);

            botThread = await _botRepository.GetBotThreadByChatId(botThread.ChatId);
            var asset = botThread.Assets.FirstOrDefault(p => p.Code == botThread.LastMessage.ToUpper());

            if (asset == null) return await Task.FromResult("Você não tem esse ativo em sua lista.");

            return await RemoveAsset(botThread, asset);
        }

        private async Task<string> RemoveAsset(BotThread botThread, Asset asset)
        {
            await _botRepository.RemoveAsset(botThread, asset);

            return await Task.FromResult($"{asset.Code} removido com sucesso!");
        }
    }
}
