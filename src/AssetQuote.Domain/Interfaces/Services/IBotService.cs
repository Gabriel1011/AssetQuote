namespace AssetQuote.Domain.Interfaces.Services;

public interface IBotService
{
    public Task<string> StartCommunication(BotThread thread);
}
