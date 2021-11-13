using System.Net.Http;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.WebScraping.Base;
public abstract class BaseWebScraping
{
    protected async Task<string> GetPage(string code, string url)
    {
        var link = string.Format(url, code);
        using var request = await new HttpClient().GetAsync(link);
        return await request.Content.ReadAsStringAsync();
    }
}
