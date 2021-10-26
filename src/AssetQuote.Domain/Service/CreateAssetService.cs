using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Entities.Enuns;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Domain.Service.Base;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service
{
    public class CreateAssetService : BaseAssetService, ICreateAssetService
    {
        private readonly IAssetService _assetService;

        public CreateAssetService(IAssetService assetService, IBotThreadRepository bot) : base(bot)
        {
            _assetService = assetService;
        }

        public async Task<string> CreateNewAsset(BotThread thread)
        {
            await UpdateStep(thread, BotStep.CreantingAsset);
            return await Task.FromResult("Favor informar o código do ativo ou da criptomoeda!!");
        }

        public async Task<string> ConfirmCreateNewAsset(BotThread thread)
        {
            await UpdateStep(thread, BotStep.ConfirmNewAsset);

            var asset = await _assetService.FindOrCreateByCode(new Asset
            {
                Code = thread.LastMessage.ToUpper(),
                Name = thread.LastMessage.ToUpper(),
            });

            await _assetService.ConnectChatAsset(thread, asset);

            return await Task.FromResult($"Deseja monitorar o ativo/criptomoeda {thread.LastMessage}?\n1 - Sim\n2 - Não");
        }

        public async Task<string> CreateNewAssetSuccess(BotThread thread)
        {
            await UpdateStep(thread, BotStep.Start);

            return await Task.FromResult("Ativo cadastrado com sucesso");
        }

    }
}
