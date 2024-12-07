
using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Domain.Entities
{
    public class MenuRolePermission : BaseEntity<Guid>
    {
        public string? RoleId { get; set; }

        public Guid MenuId { get; set; }

        public virtual Menu? Menu { get; set; }
    }
}
