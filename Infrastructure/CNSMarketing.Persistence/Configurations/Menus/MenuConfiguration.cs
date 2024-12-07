using CNSMarketing.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CNSMarketing.Domain.Entities;

namespace CNSMarketing.Persistence.Configurations
{
    public class MenuConfiguration : BaseEntityConfiguration<Menu,Guid>
    {
        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            base.Configure(builder);


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ControllerName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.ActionName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.AreaName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();

            builder.Property(x => x.Permitted)
                .IsRequired().HasDefaultValue(false);

            builder.Property(x => x.Icon) 
           .HasMaxLength(150); 

            builder.HasMany(x => x.SubMenus)
               .WithOne(x => x.Parent)
               .HasForeignKey(x => x.ParentId) 
               .OnDelete(DeleteBehavior.Restrict);

           

           


        }
    }
}
