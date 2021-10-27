using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Interfaces
{
    public interface IWebScraping
    {
        public Task UpdateQuote();
    }
}
