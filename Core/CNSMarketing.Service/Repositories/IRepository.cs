using CNSMarketing.Domain.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace CNSMarketing.Application.Repositories;

public interface IRepository<T, Val> where T : BaseEntity<Val>
{
    DbSet<T> Table { get; }
}
