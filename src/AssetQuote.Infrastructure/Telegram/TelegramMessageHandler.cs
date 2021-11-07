using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.Telegram.Base;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Telegram
{
    public class TelegramMessageHandler : TelegramBase, IBotMessage
    {
        private readonly IConsultAssetService _consultAssetService;
        private readonly IBotThreadRepository _botThreadRepository;
        public TelegramMessageHandler(IConfiguration configuration, IConsultAssetService consultAssetService, IBotThreadRepository botThreadRepository) : base(configuration)
        {
            _consultAssetService = consultAssetService;
            _botThreadRepository = botThreadRepository;
        }

        private async Task SendMessage(string chatId, string message)
        {
            await TelegramClient.SendTextMessageAsync(chatId: chatId, message);
        }

        public async Task GenerateMessage()
        {
            var chats = await _botThreadRepository.All();

            foreach (var chat in chats)
                await SendMessage(chat.ChatId, await _consultAssetService.ConsultAsset(chat));
        }
    }
}
