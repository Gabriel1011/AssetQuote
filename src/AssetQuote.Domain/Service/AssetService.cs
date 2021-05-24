using System.Collections.Generic;
using System.Threading.Tasks;
using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces;

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
            await _assetRepository.Getall();

            public async Task<Asset> AddAsset(Asset asset) =>
                await _assetRepository.Add(asset);
    }
}
