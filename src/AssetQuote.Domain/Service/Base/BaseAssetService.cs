using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Entities.Enuns;
using AssetQuote.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service.Base
{
    public abstract class BaseAssetService
    {
        private readonly IBotThreadRepository _botThreadRepository;

        protected BaseAssetService(IBotThreadRepository botThreadRepository)
        {
            _botThreadRepository = botThreadRepository;
        }

        protected async Task UpdateStep(BotThread thread, BotStep step)
        {
            thread.BotStep = step;
            await _botThreadRepository.Update(thread);
        }
    }
}
