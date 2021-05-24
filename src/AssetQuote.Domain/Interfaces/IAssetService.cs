using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetQuote.Domain.Entities;

namespace AssetQuote.Domain.Interfaces
{
    public interface IAssetService
    {
        public Task<IEnumerable<Asset>> GetAllAssets();
        public Task<Asset> AddAsset(Asset asset);
    }
}
