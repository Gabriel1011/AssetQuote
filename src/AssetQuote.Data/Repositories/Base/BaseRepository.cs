namespace AssetQuote.Data.Repositories.Base;

public class BaseRepository<T> where T : BaseEntity
{
    private readonly AssetContext _contextBase;
    private readonly DbSet<T> _dbSet;

    public AssetContext ContextoBase => _contextBase;

    public BaseRepository(AssetContext context)
    {
        _contextBase = context;
        _dbSet = _contextBase.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        _dbSet.Add(entity);

        await Commit();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);

        _dbSet.Attach(entity);
        _contextBase.Entry(entity).State = EntityState.Modified;

        await Commit();
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbSet.Attach(entity);
        _dbSet.Remove(entity);
        await Commit();
    }

    public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> filter) => await _dbSet.Where(filter).AsNoTracking().ToListAsync();
    public async Task<bool> Any(Expression<Func<T, bool>> filter) => await _dbSet.AsNoTracking().AnyAsync(filter);
    public async Task<T> Find(Guid id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    public async Task<T> FindBy(Expression<Func<T, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);
    public async Task<IEnumerable<T>> All() => await _dbSet.ToListAsync();
    public async Task DisposeAsync() => await Task.Run(() => { ContextoBase?.Dispose(); });
    private async Task DeTachLocal(Func<T, bool> filter, T entity)
    {
        var local = _contextBase.Set<T>().Local.FirstOrDefault(filter);
        if (local != null) await Task.Run(() => _contextBase.Entry(local).State = EntityState.Detached);
    }
    protected async Task Commit() => await ContextoBase.Commit();
}
