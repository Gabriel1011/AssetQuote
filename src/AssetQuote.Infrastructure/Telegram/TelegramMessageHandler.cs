using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.Telegram.Base;

namespace AssetQuote.Infrastructure.Telegram
{
    public class TelegramMessageHandler : TelegramBase, IBotMessage
    {
        public TelegramMessageHandler(IConfiguration configuration) : base(configuration) { }

        public async Task SendMessage(string chatId, string message)
        {
            await TelegramClient.SendTextMessageAsync(chatId: chatId, message);
        }
    }
}
