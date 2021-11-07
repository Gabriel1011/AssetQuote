using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Repositories
{
    public interface IBotThreadRepository : IBaseRepository<BotThread>
    {
        public Task<BotThread> GetBotThreadByChatId(string chatId);
        public Task<bool> RemoveAsset(BotThread botThread, Asset asset);
        public Task<IEnumerable<BotThread>> GetAll();
    }
}
