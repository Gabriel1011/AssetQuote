namespace AssetQuote.Domain.Entities;

public class Asset : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public double Value { get; set; }
    public double Percentage { get; set; }
    public double OcillationValue { get; set; }
    public List<BotThread> BotThreads { get; set; }
}
