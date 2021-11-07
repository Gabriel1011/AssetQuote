using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface IBotMessage
    {
        Task SendMessage(string chatId, string message);
    }
}
