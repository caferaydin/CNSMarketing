using CNSMarketing.Application.Models.Responses.Authentication;

namespace CNSMarketing.Application.Abstraction.Service.UserRole;

public interface IRoleService
{
    (IEnumerable<RoleResponseModel>, int) GetAllRoles(int page, int size);
    Task<(string id, string name)> GetRoleById(string id);
    Task<bool> CreateRole(string name, string description);
    Task<bool> DeleteRole(string id);
    Task<bool> UpdateRole(string id, string name);
}

