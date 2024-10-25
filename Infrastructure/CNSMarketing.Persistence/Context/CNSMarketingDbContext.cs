using System;
using System.Reflection;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Domain.Entity.Manager;
using CNSMarketing.Domain.Entity.SocialMedia;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CNSMarketing.Persistence.Context;

public class CNSMarketingDbContext : IdentityDbContext<AppUser, AppRole, string>
{

    public CNSMarketingDbContext(DbContextOptions options) : base(options) { }


    #region  DataTable

    public DbSet<Customer> Customers { get; set; }
    public DbSet<API> API { get; set; }
    public DbSet<APIToken> APIToken { get; set; }

    #endregion



    #region  Reflection 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #endregion
    
    #region  Interceptor

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlar.
        var datas = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity<Guid> || e.Entity is BaseEntity<int>);

        foreach (var data in datas)
        {
            if (data.Entity is BaseEntity<Guid> baseEntityGuid)
            {
                _ = data.State switch
                {
                    EntityState.Added => baseEntityGuid.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => baseEntityGuid.UpdatedDate = DateTime.UtcNow,
                    EntityState.Deleted => baseEntityGuid.DeletedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            else if (data.Entity is BaseEntity<int> baseEntityInt)
            {
                _ = data.State switch
                {
                    EntityState.Added => baseEntityInt.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => baseEntityInt.UpdatedDate = DateTime.UtcNow,
                    EntityState.Deleted => baseEntityInt.DeletedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    #endregion
}
