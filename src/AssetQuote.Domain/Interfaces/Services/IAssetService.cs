using AssetQuote.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface IAssetService
    {
        public Task<IEnumerable<Asset>> GetAllAssets();
        public Task<Asset> AddAsset(Asset asset);
        public Task<Asset> FindOrCreateByCode(Asset asset);
        public Task ConnectChatAsset(BotThread thread, Asset asset);
    }
}
