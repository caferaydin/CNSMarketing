using CNSMarketing.Domain.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNSMarketing.Persistence.Configurations
{
    public class BaseEntityConfiguration<Entity, Val> : IEntityTypeConfiguration<Entity> where Entity : BaseEntity<Val>
    {
        public virtual void Configure(EntityTypeBuilder<Entity> builder)
        {
            var idProperty = typeof(Entity).GetProperty("Id");

            if (idProperty!.PropertyType == typeof(Guid))
            {
                builder.HasKey(bec => bec.Id);
                builder.Property(bec => bec.Id)
                    .HasDefaultValueSql("NEWID()");
            }
            else
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityColumn();
            }

            builder.Property(bec => bec.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(bec => bec.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(bec => bec.UpdatedDate).IsRequired(false);
            builder.Property(bec => bec.DeletedDate).IsRequired(false);
        }
    }
}
