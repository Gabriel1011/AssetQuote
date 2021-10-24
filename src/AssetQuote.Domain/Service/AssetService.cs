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
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<IEnumerable<Asset>> GetAllAssets() =>
            await _assetRepository.All();

            public async Task<Asset> AddAsset(Asset asset) =>
                await _assetRepository.Create(asset);
    }
}
