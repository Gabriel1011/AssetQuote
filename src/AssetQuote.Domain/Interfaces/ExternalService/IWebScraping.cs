
namespace AssetQuote.Domain.Interfaces.ExternalService;

public interface IWebScraping
{
    public Task UpdateQuote(Asset asset);
    public Task UpdateAllQuote();
}
