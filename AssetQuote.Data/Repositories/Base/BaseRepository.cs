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
        private readonly AssetContext _contextoBase;
        private readonly DbSet<T> _entidade;

        public AssetContext ContextoBase => _contextoBase;

        public BaseRepository(AssetContext context)
        {
            _contextoBase = context;
            _entidade = _contextoBase.Set<T>();
        }

        public async Task<T> Create(T entidade)
        {
            _entidade.Add(entidade);
            await ContextoBase.Commit();
            return entidade;
        }

        public async Task<T> Update(T entidade)
        {
            _entidade.Update(entidade);
            await ContextoBase.Commit();
            return entidade;
        }

        public async Task Delete(T entidade)
        {
            _entidade.Remove(entidade);
            await ContextoBase.Commit();
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> filter) => await _entidade.Where(filter).AsNoTracking().ToListAsync();
        public async Task<T> FindBy(Expression<Func<T, bool>> filter) => await _entidade.FirstOrDefaultAsync(filter);
        public async Task<bool> Any(Expression<Func<T, bool>> filter) => await _entidade.AsNoTracking().AnyAsync(filter);
        public async Task<T> Find(Guid id) => await _entidade.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        public async Task<IEnumerable<T>> All() => await _entidade.AsNoTracking().ToListAsync();
        public async Task DisposeAsync() => await Task.Run(() => { ContextoBase?.Dispose();  }); 

    }
}
