namespace AssetQuote.Domain.Entities;

public class AssetBotThread : BaseEntity
{
    public Guid AssetsId { get; set; }
    public Asset Asset { get; set; }
    public Guid BotThreadsId { get; set; }
    public BotThread BotThread { get; set; }
}
