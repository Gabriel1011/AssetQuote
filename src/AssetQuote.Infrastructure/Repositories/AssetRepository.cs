using System.Collections.Generic;
using System.Threading.Tasks;
using AssetQuote.Domain.Entities;
using AssetQuote.Domain.Interfaces;
using AssetQuote.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetQuote.Infrastructure.Repositories
{
  public class AssetRepository : IAssetRepository
  {
    private readonly AssetContext _context;
    public AssetRepository(AssetContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Asset>> Getall() => await _context.Asset.ToListAsync();

    public async Task<Asset> Add (Asset asset){
      await _context.Asset.AddAsync(asset);
      await _context.SaveChangesAsync();
      return asset;
    }
  }
}
