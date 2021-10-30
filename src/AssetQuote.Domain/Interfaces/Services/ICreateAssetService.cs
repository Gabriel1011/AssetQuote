using AssetQuote.Domain.Entities;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface ICreateAssetService
    {
        public Task<string> CreateNewAsset(BotThread thread);
        public Task<string> CreatingNewAsset(BotThread thread);
    }
}
