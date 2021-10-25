using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface IBot
    {
        public Task Send();
    }
}
