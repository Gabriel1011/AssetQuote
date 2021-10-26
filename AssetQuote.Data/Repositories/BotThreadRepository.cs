using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AssetQuote.Data.Repositories
{
    public class BotThreadRepository : BaseRepository<BotThread>, IBotThreadRepository
    {
        private readonly AssetContext _context;
        public BotThreadRepository(AssetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BotThread> GetBotThreadByChatId(string chatId) =>
            await _context.BotThread
            .Include(p => p.Assets)
            .FirstOrDefaultAsync(p => p.ChatId == chatId);
    }
}
