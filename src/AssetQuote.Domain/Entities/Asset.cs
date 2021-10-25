namespace AssetQuote.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public Asset(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; private set; }
        public string Code { get; set; }
    }
}
