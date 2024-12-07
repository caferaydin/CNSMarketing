namespace CNSMarketing.Application.Models.Requests.Manager.Menus
{
    public class UpdateMenuRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? AreaName { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public bool Permitted { get; set; } = true;
        public string? Icon { get; set; } // Yeni eklenen özellik
        public Guid? ParentId { get; set; } // Üst menü ID'si
    }
}
