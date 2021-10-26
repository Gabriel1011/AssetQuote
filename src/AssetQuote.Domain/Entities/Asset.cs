using System.Collections.Generic;

namespace AssetQuote.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<BotThread> BotThreads { get; set; }
    }
}
