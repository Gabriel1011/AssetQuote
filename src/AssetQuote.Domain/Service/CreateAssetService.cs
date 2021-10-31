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
            return await Task.FromResult("Favor informar o código do ativo ou da criptomoeda, é possível cadastrar vários ativos seprando-os por virgula. \n\n ex: VALE3,PETR4,MXRF11");
        }

        public async Task<string> CreatingNewAsset(BotThread thread)
        {
            await UpdateStep(thread, BotStep.Start);

            var assets = thread.LastMessage.Split(',');

            foreach (var newAsset in assets)
            {
                var asset = await _assetService.FindOrCreateByCode(new Asset
                {
                    Code = newAsset.ToUpper(),
                    Name = newAsset.ToUpper(),
                });

                await _assetService.ConnectChatAsset(thread, asset);
            }           

            return await Task.FromResult($"{thread.LastMessage} criado com sucesso!!");
        }

    }
}
