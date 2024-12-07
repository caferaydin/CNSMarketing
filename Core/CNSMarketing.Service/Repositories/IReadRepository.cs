using System.Linq.Expressions;
using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Application.Repositories;

public interface IReadRepository<T, Val> : IRepository<T, Val> where T : BaseEntity<Val>
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetByIdAsync(Val id, bool tracking = true);
}