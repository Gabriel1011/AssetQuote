using AssetQuote.Domain.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AssetQuote.Infrastructure.Telegram
{
    public class TelegramBot : IBot
    {
        private readonly IBotService _botService;

        public TelegramBot(IBotService botService)
        {
            _botService = botService;
        }

        public async Task Send()
        {
            var botClient = new TelegramBotClient("{BOT-KEY}");

            var me = await botClient.GetMeAsync();
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            using var cts = new CancellationTokenSource();

            // StartReceiving não bloqueia o thread do chamador. O recebimento é feito no ThreadPool.
            botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Envie um pedido de cancelamento para parar o bot
            cts.Cancel();

            Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Type != UpdateType.Message)
                    return;
                if (update.Message.Type != MessageType.Text)
                    return;

                var chatId = update.Message.Chat.Id;

                Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}.");

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    await _botService.StartContact()
                );
            }
        }
    }
}
