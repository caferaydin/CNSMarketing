using CNSMarketing.Application.DTOs;
using CNSMarketing.Application.Models.DTOs.Menus;

namespace CNSMarketing.Application.ViewModels
{
    public class AssignRoleToMenuViewModel
    {
        public List<MenuDTO> Menus { get; set; } = new List<MenuDTO>();
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

   
}
