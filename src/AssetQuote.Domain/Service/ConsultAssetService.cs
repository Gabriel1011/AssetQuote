namespace AssetQuote.Domain.Service;
public class ConsultAssetService : BaseAssetService, IConsultAssetService
{
    private readonly IBotThreadRepository _botRepository;
    public ConsultAssetService(IBotThreadRepository bot) : base(bot)
    {
        _botRepository = bot;
    }

    public async Task<string> ConsultAsset(BotThread thread)
    {
        await UpdateStep(thread, BotStep.Start);

        thread = await _botRepository.GetBotThreadByChatId(thread.ChatId);

        return await GenerateAssetList(thread.Assets);
    }

    private async Task<string> GenerateAssetList(IEnumerable<Asset> assets)
    {
        if(!assets.Any())
            return await Task.FromResult("Para consultar é necessário cadastrar um ativo antes.");

        List<string> messageAsset = new();

        foreach (var asset in assets)
        {
            var ocillationIcon = await GenerateIndicateIcon(asset.OcillationValue);
            messageAsset.Add(string.Format($"{ocillationIcon} Ativo: {asset.Code} Valor: R$ {asset.Value} \nPorcentagem: {asset.Percentage}% Valor Ocilação: R$ {asset.OcillationValue}"));
        }
        
        return await Task.FromResult(string.Join("\n\n", messageAsset));
    }

    async Task<string> GenerateIndicateIcon(double ocillagePercentage) => await Task.FromResult(ocillagePercentage == 0 ? "🟠" : ocillagePercentage > 0 ? "🟢" : "🔴");
}
