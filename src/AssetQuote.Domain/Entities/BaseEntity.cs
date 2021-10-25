using System;

namespace AssetQuote.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
