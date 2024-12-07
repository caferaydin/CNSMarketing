using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Application.Abstraction.Service.UserRole;
using CNSMarketing.Application.Models.Responses.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CNSMarketing.Persistence.Service.Authentication
{
    public class RoleService : IRoleService
    {
        #region Fields & Ctor
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion

        #region Methods
        public async Task<bool> CreateRole(string name, string description)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name, Description = description });

            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public (IEnumerable<RoleResponseModel>, int) GetAllRoles(int page, int size)
        {
            var query = _roleManager.Roles;

            IQueryable<AppRole> rolesQuery = null;

            if (page != -1 && size != -1)
                rolesQuery = query.Skip(page * size).Take(size);
            else
                rolesQuery = query;

            var roles = rolesQuery
                .Select(r => new RoleResponseModel { Id = r.Id, Name = r.Name, Description = r.Description })
                .ToList();

            // Toplam sayıyı al
            var totalCount = query.Count();

            return (roles, totalCount);
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
            //var role = await _roleManager.GetRoleIdAsync(new() { Id = id});
            var item = await _roleManager.FindByIdAsync(id);

            return (item.Id, item.Name);
        }

        public async Task<bool> UpdateRole(string id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        #endregion
    }
}
