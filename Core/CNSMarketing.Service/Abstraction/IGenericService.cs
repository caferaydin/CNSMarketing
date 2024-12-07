using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Repositories;
using System.Linq.Expressions;

namespace CNSMarketing.Application.Abstraction
{
    public interface IGenericService<TEntity, Key, IRead, IWrite>
        where TEntity : BaseEntity<Key>
        where IRead : IReadRepository<TEntity, Key>
        where IWrite : IWriteRepository<TEntity, Key>

    {
        Task<TEntity> GetByIdAsync(Key id);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true);
        IQueryable<TEntity> GetAll(bool tracking = true);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(List<TEntity> datas);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> RemoveAsync(Key id);
        Task<bool> RemoveRange(List<TEntity> datas);
    }
}
