using CNSMarketing.Domain.Entities;
using CNSMarketing.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CNSMarketing.Persistence.Configurations
{
    public class MenuRolePermissionConfiguration : BaseEntityConfiguration<MenuRolePermission,Guid>
    {
        public override void Configure(EntityTypeBuilder<MenuRolePermission> builder)
        {
            base.Configure(builder);
       
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleId)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.MenuId)
                .IsRequired();

            builder.HasOne(mrp => mrp.Menu)
              .WithMany(menu => menu.MenuRolePermissions)
              .HasForeignKey(mrp => mrp.MenuId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(mrp => new { mrp.RoleId, mrp.MenuId })
             .IsUnique();


        }
    }
}
