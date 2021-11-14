namespace AssetQuote.Data.Repositories;

public class AssetRepository : BaseRepository<Asset>, IAssetRepository
{
    private readonly AssetContext _context;
    public AssetRepository(AssetContext context) : base(context)
    {
        _context = context;
    }
}
