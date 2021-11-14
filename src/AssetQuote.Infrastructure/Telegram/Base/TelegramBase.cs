using Telegram.Bot;
using User = Telegram.Bot.Types.User;

namespace AssetQuote.Infrastructure.Telegram.Base
{
    public abstract class TelegramBase
    {
        protected TelegramBotClient TelegramClient { get; set; }
        protected User Me { get; set; }

        protected TelegramBase(IConfiguration configuration)
        {
            TelegramClient = new TelegramBotClient(configuration["BotTelegram:BotKey"]);
        }

        protected async Task<User> GetMe() => Me = await TelegramClient.GetMeAsync();
    }
}
