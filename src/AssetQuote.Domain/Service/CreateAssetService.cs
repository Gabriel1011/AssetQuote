using AssetQuote.Domain.Interfaces.ExternalService;

namespace AssetQuote.Domain.Service;
public class CreateAssetService : BaseAssetService, ICreateAssetService
{
    private readonly IAssetService _assetService;
    private readonly IWebScraping _webScraping;

    public CreateAssetService(IAssetService assetService, 
        IBotThreadRepository bot, 
        IWebScraping webScraping) : base(bot)
    {
        _assetService = assetService;
        _webScraping = webScraping;
    }

    public async Task<string> CreateNewAsset(BotThread thread)
    {
        await UpdateStep(thread, BotStep.CreantingAsset);
        return await Task.FromResult("Favor informar o código do ativo ou da criptomoeda, é possível cadastrar vários ativos seprando-os por virgula. \n\n ex: VALE3,PETR4,MXRF11");
    }

    public async Task<string> CreatingNewAsset(BotThread thread)
    {
        await UpdateStep(thread, BotStep.Start);

        var assets = thread.LastMessage.Split(',');

        foreach (var newAsset in assets)
        {
            var asset = await _assetService.FindOrCreateByCode(new Asset
            {
                Code = newAsset.ToUpper(),
                Name = newAsset.ToUpper(),
            });

            await _assetService.ConnectChatAsset(thread, asset);

            await _webScraping.UpdateQuote(asset);
        }

        return await Task.FromResult($"{thread.LastMessage} criado com sucesso!!");
    }

}