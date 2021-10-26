using AssetQuote.Domain.Entities.Enuns;
using System.Collections.Generic;

namespace AssetQuote.Domain.Entities
{
    public class BotThread : BaseEntity
    {
        public string ChatId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Cellphone { get; set; }
        public BotStep BotStep { get; set; }
        public string LastMessage { get; set; }
        public List<Asset> Assets { get; set; }
    }
}
