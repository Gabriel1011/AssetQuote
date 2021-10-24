using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetQuote.Data.Repositories
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {

        public AssetRepository(AssetContext context) : base(context)
        {

        }

        public Task<Asset> Add(Asset asset)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Asset>> Getall()
        {
            throw new NotImplementedException();
        }
    }
}
