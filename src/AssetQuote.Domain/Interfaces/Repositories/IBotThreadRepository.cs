using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories.Base;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Repositories
{
    public interface IBotThreadRepository : IBaseRepository<BotThread>
    {
        public Task<BotThread> GetBotThreadByChatId(string chatId);
    }
}
