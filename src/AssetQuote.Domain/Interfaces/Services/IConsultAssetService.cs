namespace AssetQuote.Domain.Interfaces.Services;

public interface IConsultAssetService
{
    public Task<string> ConsultAsset(BotThread thread);
}
