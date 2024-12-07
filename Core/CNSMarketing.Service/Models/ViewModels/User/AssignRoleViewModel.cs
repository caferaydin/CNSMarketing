using CNSMarketing.Domain.Entity.Authentication;

namespace CNSMarketing.Application.Models.ViewModels.User
{
    public class AssignRoleViewModel
    {
        public List<AppUser>? Users { get; set; }
        public List<AppRole>? Roles { get; set; }
    }
}
