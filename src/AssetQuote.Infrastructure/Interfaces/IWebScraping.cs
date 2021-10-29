using AssetQuote.Domain.Entities;
using System.Threading.Tasks;

namespace AssetQuote.Infrastructure.Interfaces
{
    public interface IWebScraping
    {
        public Task UpdateQuote();
    }
}
