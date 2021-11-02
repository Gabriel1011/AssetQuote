using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetQuote.Data.Repositories
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        private readonly AssetContext _context;
        public AssetRepository(AssetContext context) : base(context)
        {
            _context = context;
        }
    }
}
