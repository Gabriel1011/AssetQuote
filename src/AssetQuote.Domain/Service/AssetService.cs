using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service
{
    public class AssetService : IAssetService
    {
        IAssetRepository _assetRepository;
        IBotThreadRepository _botThreadRepository;
        public AssetService(IAssetRepository assetRepository, IBotThreadRepository botThreadRepository)
        {
            _assetRepository = assetRepository;
            _botThreadRepository = botThreadRepository;
        }

        public async Task<IEnumerable<Asset>> GetAllAssets() =>
            await _assetRepository.All();

        public async Task<Asset> AddAsset(Asset asset) =>
            await _assetRepository.Create(asset);

        public async Task<Asset> FindOrCreateByCode(Asset asset)
        {
            var assetBase = await _assetRepository.FindBy(p => p.Code == asset.Code);

            if (assetBase == null) assetBase = await _assetRepository.Create(asset);

            return await Task.FromResult(assetBase);
        }

        public async Task ConnectChatAsset(BotThread thread, Asset asset)
        {
            thread.Assets ??= new List<Asset>();
            thread.Assets.Add(asset);
            await _botThreadRepository.Update(thread);
        }       
    }
}
