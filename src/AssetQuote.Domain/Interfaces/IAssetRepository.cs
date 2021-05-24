using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetQuote.Domain.Entities;

namespace AssetQuote.Domain.Interfaces
{
    public interface IAssetRepository
    {
        public Task<IEnumerable<Asset>> Getall();
        public Task<Asset> Add (Asset asset);
    }
}
