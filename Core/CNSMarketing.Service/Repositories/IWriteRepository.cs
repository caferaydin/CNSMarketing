using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Service.Repositories;

public interface IWriteRepository<T, Val> : IRepository<T, Val> where T : BaseEntity<Val>
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(Val id);
        bool Update(T model);
        Task<int> SaveAsync();
    }
