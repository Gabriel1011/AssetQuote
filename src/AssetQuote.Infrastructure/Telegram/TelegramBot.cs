using AssetQuote.Domain.Entities.Enuns;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Infrastructure.Telegram.Base;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AssetQuote.Infrastructure.Telegram
{
    public class TelegramBot : TelegramBase, IBot
    {
        private readonly IBotService _botService;
        private readonly ILogger _lloger;

        private Message _message;

        public TelegramBot(IBotService botService, IConfiguration configuration, ILogger<TelegramBot> lloger) : base(configuration)
        {
            _botService = botService;
            _lloger = lloger;
        }

        public async Task Send()
        {
            var me = await GetMe();
            
            _lloger.LogInformation($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");            

            using var cts = new CancellationTokenSource();

            // StartReceiving não bloqueia o thread do chamador. O recebimento é feito no ThreadPool.
            TelegramClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);

            _lloger.LogInformation($"Start listening for @{me.Username}");
            Console.ReadKey();

            // Envie um pedido de cancelamento para parar o bot
            cts.Cancel();

            Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                SentrySdk.CaptureException(exception);

                _lloger.LogError(ErrorMessage);
                return Task.CompletedTask;
            }

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Type != UpdateType.Message)
                    return;
                if (update.Message.Type != MessageType.Text)
                    return;

                _message = update.Message;

                _lloger.LogInformation($"Received a '{_message.Text}' message in chat {_message.Chat.Id}.");

                await botClient.SendTextMessageAsync(chatId: _message.Chat.Id, await StartCommunication(), cancellationToken: cancellationToken);
            }
            
            async Task<string> StartCommunication() => await _botService.StartCommunication(await BotThreadGenerate());

            async Task<BotThread> BotThreadGenerate() => await Task.FromResult(new BotThread
            {
                ChatId = _message.Chat.Id.ToString(),
                FirstName = _message.From.FirstName,
                LastName = _message.From.LastName,
                UserName = _message.From.Username,
                LastMessage = _message.Text,
                BotStep = BotStep.Start
            });
        }
    }
}
