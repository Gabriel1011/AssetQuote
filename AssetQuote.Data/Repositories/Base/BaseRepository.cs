using AssetQuote.Domain.Entities;
using AssetQuote.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AssetQuote.Data.Repositories
{
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

            await ContextoBase.Commit();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);

            _dbSet.Attach(entity);
            _contextBase.Entry(entity).State = EntityState.Modified;

            await ContextoBase.Commit();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            await ContextoBase.Commit();
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> filter) => await _dbSet.Where(filter).AsNoTracking().ToListAsync();
        public async Task<T> FindBy(Expression<Func<T, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);
        public async Task<bool> Any(Expression<Func<T, bool>> filter) => await _dbSet.AsNoTracking().AnyAsync(filter);
        public async Task<T> Find(Guid id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        public async Task<IEnumerable<T>> All() => await _dbSet.ToListAsync();
        public async Task DisposeAsync() => await Task.Run(() => { ContextoBase?.Dispose();  }); 

    }
}
