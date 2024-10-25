using CNSMarketing.Domain.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace CNSMarketing.Service.Repositories;

public interface IRepository<T, Val> where T : BaseEntity<Val>
{
    DbSet<T> Table { get; }
}
