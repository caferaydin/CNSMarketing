using CNSMarketing.Application.Repositories;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CNSMarketing.Persistence.Repositories.Manager
{
    public class MenuRolePermissionWriteRepository : WriteRepository<MenuRolePermission, Guid>, IMenuRolePermissionWriteRepository
    {
        private readonly CNSMarketingDbContext _context;
        public MenuRolePermissionWriteRepository(CNSMarketingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetPermissionsByRoleIdAsync(string roleId, IEnumerable<Guid> permissionIds)
        {
            if (string.IsNullOrWhiteSpace(roleId) || permissionIds == null || !permissionIds.Any())
            {
                return false;
            }

            // Mevcut izinleri al
            var existingPermissions = await _context.MenuRolePermissions
                .Where(x => x.RoleId == roleId)
                .ToListAsync();

            // Yeni izinleri belirle
            var newPermissionIds = permissionIds.ToHashSet();
            var existingPermissionIds = existingPermissions.Select(ep => ep.MenuId).ToHashSet();

            // Silinmesi gereken izinler
            var permissionsToRemove = existingPermissions
                .Where(ep => !newPermissionIds.Contains(ep.MenuId))
                .ToList();

            // Eklenecek yeni izinler
            var permissionsToAdd = newPermissionIds
                .Except(existingPermissionIds)
                .Select(permissionId => new MenuRolePermission
                {
                    RoleId = roleId,
                    MenuId = permissionId,
                    IsActive = true
                })
                .ToList();

            // Mevcut izinleri kaldır
            _context.MenuRolePermissions.RemoveRange(permissionsToRemove);

            // Yeni izinleri ekle
            await _context.MenuRolePermissions.AddRangeAsync(permissionsToAdd);

            // Değişiklikleri kaydet
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
