using AssetQuote.Domain.Entities;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface ICreateAssetService
    {
        public Task<string> CreateNewAsset(BotThread thread);
        public Task<string> ConfirmCreateNewAsset(BotThread thread);
        public Task<string> CreateNewAssetSuccess(BotThread thread);
    }
}
