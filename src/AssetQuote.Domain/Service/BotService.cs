namespace AssetQuote.Domain.Service;
public class BotService : BaseAssetService, IBotService
{
    private readonly IAssetService _assetService;
    private readonly IBotThreadRepository _botThreadRepository;
    private readonly ICreateAssetService _createAssetService;
    private readonly IConsultAssetService _consultAssetService;
    private readonly IRemoveAssetService _removeAssetService;


    public BotService(IAssetService assetService,
        ICreateAssetService createAssetService,
        IConsultAssetService consultAssetService,
        IRemoveAssetService removeAssetService,
        IBotThreadRepository bot) : base(bot)
    {
        _assetService = assetService;
        _botThreadRepository = bot;
        _createAssetService = createAssetService;
        _consultAssetService = consultAssetService;
        _removeAssetService = removeAssetService;
    }

    public async Task<string> Process(BotThread thread) => thread.BotStep switch
    {
        BotStep.Start => await AnswerStart(thread),
        BotStep.NewAsset => await _createAssetService.CreateNewAsset(thread),
        BotStep.CreantingAsset => await _createAssetService.CreatingNewAsset(thread),
        BotStep.RemoveAsset => await _removeAssetService.StartDeletation(thread),
        BotStep.ConsultAsset => await _consultAssetService.ConsultAsset(thread),
        BotStep.ConfirmRemoveAsset => await _removeAssetService.ConfirmDeletation(thread),
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

        var steps = (BotStep[])Enum.GetValues(typeof(BotStep));

        return thread.BotStep == BotStep.Default || !steps.Contains(thread.BotStep) ? await StartContact() : await Process(thread);
    }

}
