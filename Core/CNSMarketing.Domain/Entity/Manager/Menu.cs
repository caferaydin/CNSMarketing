using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Domain.Entities
{
    public class Menu : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? AreaName { get; set; }
        public int DisplayOrder { get; set; }
        public bool Permitted { get; set; }
        public string? Icon { get; set; } 
        public Guid? ParentId { get; set; } 
        public Menu? Parent { get; set; } 
        public  List<Menu> SubMenus { get; set; } = new List<Menu>(); 
        public ICollection<MenuRolePermission> MenuRolePermissions { get; set; }
    }
}
