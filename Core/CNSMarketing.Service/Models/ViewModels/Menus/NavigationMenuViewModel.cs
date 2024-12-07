namespace CNSMarketing.Application.ViewModels
{
    public class NavigationMenuViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; } // Nullable olarak ayarla
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Area { get; set; }
        public bool Permitted { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public string? Icon { get; set; } // Yeni eklenen özellik
        public List<NavigationMenuViewModel> SubMenus { get; set; } = new List<NavigationMenuViewModel>();
    }
}
