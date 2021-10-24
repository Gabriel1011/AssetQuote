namespace AssetQuote.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; set; }
    }
}
