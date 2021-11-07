using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<BotThread> GetBotThreadByChatId(string chatId)
        {
            await _context.DetachAllEntities();

            return await _context.BotThread
            .Include(p => p.Assets)
            .FirstOrDefaultAsync(p => p.ChatId == chatId);
        }

        public async Task<IEnumerable<BotThread>> GetAll()
        {
            await _context.DetachAllEntities();

            return await _context.BotThread
            .Include(p => p.Assets)
            .ToListAsync();
        }

        public async Task<bool> RemoveAsset(BotThread botThread, Asset asset)
        {
            botThread = await GetBotThreadByChatId(botThread.ChatId);

            botThread.Assets.Remove(botThread.Assets.FirstOrDefault(p => p.Code == asset.Code));

            await Commit();

            return await Task.FromResult(true);
        }
    }
}
