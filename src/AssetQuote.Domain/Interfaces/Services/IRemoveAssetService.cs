using AssetQuote.Domain.Entities;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface IRemoveAssetService
    {
        public Task<string> StartDeletation(BotThread botThread);
        public Task<string> ConfirmDeletation(BotThread botThread);
    }
}
