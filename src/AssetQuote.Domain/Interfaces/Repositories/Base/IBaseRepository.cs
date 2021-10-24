using AssetQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<T> Create(T entidade);
        public Task<T> Update(T entidade);
        public Task Delete(T entidade);
        public Task<IEnumerable<T>> Search(Expression<Func<T, bool>> filter);
        public Task<T> Find(Guid id);
        public Task<IEnumerable<T>> All();
        public Task DisposeAsync();
    }
}
