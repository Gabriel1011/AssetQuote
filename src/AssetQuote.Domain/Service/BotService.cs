using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Entities.Enuns;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using AssetQuote.Domain.Service.Base;
using System;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service
{
    public class BotService : BaseAssetService, IBotService
    {
        private readonly IAssetService _assetService;
        private readonly IBotThreadRepository _botThreadRepository;
        private readonly ICreateAssetService _createAssetService;
        private readonly IConsultAssetService _consultAssetService;


        public BotService(IAssetService assetService, 
            ICreateAssetService createAssetService, 
            IConsultAssetService consultAssetService,
            IBotThreadRepository bot) : base(bot)
        {
            _assetService = assetService;
            _botThreadRepository = bot;
            _createAssetService = createAssetService;
            _consultAssetService = consultAssetService;
        }

        public async Task<string> Process(BotThread thread) => thread.BotStep switch
        {
            BotStep.Start => await AnswerStart(thread),
            BotStep.NewAsset => await _createAssetService.CreateNewAsset(thread),
            BotStep.CreantingAsset => await _createAssetService.ConfirmCreateNewAsset(thread),
            BotStep.ConfirmNewAsset => await _createAssetService.CreateNewAssetSuccess(thread),
            BotStep.RemoveAsset => await Task.Run(() => { return "Remover Asset"; }),
            BotStep.ConsultAsset => await _consultAssetService.ConsultAsset(thread),
            _ => await AnswerStart(thread)
        };

        public async Task<string> StartCommunication(BotThread thread)
        {
            string lastmessage = thread.LastMessage;

            thread = await _botThreadRepository.FindBy(p => p.ChatId == thread.ChatId) ?? await _botThreadRepository.Create(thread);

            thread.LastMessage = lastmessage;

            return await Process(thread);
        }

        private async Task<string> StartContact() => await Task.Run(() =>
        "Olá, você Gostaria de fazer o que? \n 1 - Cadastrar novo ativo \n 2 - Consultar ativos cadastrados \n 3 - Deletar ativo cadastrado \n 0 - Para Cancelar a qualquer momento!");

        private async Task<string> AnswerStart(BotThread thread)
        {
            thread.BotStep = Enum.TryParse(thread.LastMessage, true, out BotStep _) ?
                (BotStep)Enum.Parse(typeof(BotStep), thread.LastMessage) :
                BotStep.Default;

            return thread.BotStep == BotStep.Default ? await StartContact() : await Process(thread);
        }

    }
}
