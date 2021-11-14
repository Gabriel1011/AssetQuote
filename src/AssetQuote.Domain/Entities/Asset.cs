namespace AssetQuote.Domain.Entities;

public class Asset : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public double Valor { get; set; }
    public double Porcentagem { get; set; }
    public double ValorOcilacao { get; set; }
    public List<BotThread> BotThreads { get; set; }
}
