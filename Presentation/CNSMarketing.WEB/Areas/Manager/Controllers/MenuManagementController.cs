using CNSMarketing.Application.Abstraction.Service.Manager;
using CNSMarketing.Application.DTOs;
using CNSMarketing.Application.Features.Queries.Role.GetRoles;
using CNSMarketing.Application.Models.DTOs.Menus;
using CNSMarketing.Application.Models.Requests.Manager.Menus;
using CNSMarketing.Application.ViewModels;
using CNSMarketing.Domain.Entities;
using CNSMarketing.WEB.Filters;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CNSMarketing.WEB.Areas.Manager.Controllers
{
    [Area("Manager")]
    [DynamicAuthorization]
    public class MenuManagementController : Controller
    {
        private readonly ILogger<MenuManagementController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMenuService _menuService;

        private readonly INavigationMenuService _navigationMenuService;
        public MenuManagementController(ILogger<MenuManagementController> logger, IMapper mapper, IMediator mediator, INavigationMenuService navigationMenuService, IMenuService menuService)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            _navigationMenuService = navigationMenuService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _menuService.GetAllAsync();
            //var menu = _mapper.Map<IEnumerable<MenuDTO>>(models); 
            // mapstermapper error enumarable property

            return View(models);
        }


        #region Menu Model

        [HttpPost]
        public async Task<IActionResult> AddMenu(CreateMenuRequest createMenu)
        {
            if (ModelState.IsValid)
            {

                var menu = new Menu()
                {
                    Name = createMenu.Name,
                    ControllerName = createMenu.ControllerName != null ? createMenu.ControllerName : "",
                    ActionName = createMenu.ActionName != null ? createMenu.ActionName : "",
                    AreaName = "",
                    DisplayOrder = createMenu.DisplayOrder,
                    Permitted = false,
                    Icon = createMenu.Icon,
                    ParentId = createMenu.ParentId,
                };
                await _menuService.AddAsync(menu);

                return Redirect(Request.Headers["Referer"].ToString());
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMenu(UpdateMenuRequest updateMenu)
        {
            var existMenu = await _menuService.GetByIdAsync(updateMenu.Id);
            if (existMenu == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var menu = _mapper.Map<Menu>(updateMenu);
                await _menuService.UpdateAsync(menu);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Redirect(Request.Headers["Referer"].ToString());

        }
        [HttpPost]
        public async Task<IActionResult> DeleteMenu(string id)
        {
            var Menu = await _menuService.GetByIdAsync(Guid.Parse(id));
            if (Menu == null)
            {
                return NotFound();
            }
            await _menuService.DeleteAsync(Menu);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        #endregion  

        #region Menu Role Management

        public async Task<IActionResult> AssignRoleToMenu()
        {
            // Tüm rolleri al
            var roles = await _mediator.Send(new GetRolesQueryRequest());

            // Tüm menüleri al
            var menus = await _menuService.GetMenusAsync();

            // İlk role göre menü izinlerini al (sayfa ilk yüklendiğinde)
            var rolePermissions = new List<NavigationMenuViewModel>();
            if (roles.Roles.Any())
            {
                rolePermissions = await _navigationMenuService.GetPermissionsByRoleIdAsync(roles.Roles.First().Id);
            }

            // ViewModel oluştur
            var model = new AssignRoleToMenuViewModel
            {
                Roles = roles.Roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name, Description = r.Description }).ToList(),
                Menus = menus.Select(m => new MenuDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    ControllerName = m.ControllerName,
                    ActionName = m.ActionName,
                    AreaName = m.AreaName,
                    DisplayOrder = m.DisplayOrder,
                    Permitted = rolePermissions.Any(p => p.Id == m.Id),
                    Icon = m.Icon,
                    ParentId = m.ParentId,
                    IsActive = m.IsActive,
                    CreatedDate = m.CreatedDate,
                    UpdatedDate = m.UpdatedDate,
                    DeletedDate = m.DeletedDate,
                    SubMenus = m.SubMenus.Any() ? m.SubMenus.Select(sm => new MenuDTO
                    {
                        Id = sm.Id,
                        Name = sm.Name,
                        ControllerName = sm.ControllerName,
                        ActionName = sm.ActionName,
                        AreaName = sm.AreaName,
                        DisplayOrder = sm.DisplayOrder,
                        Permitted = rolePermissions.Any(p => p.Id == sm.Id),
                        Icon = sm.Icon,
                        ParentId = sm.ParentId,
                        IsActive = sm.IsActive,
                        CreatedDate = sm.CreatedDate,
                        UpdatedDate = sm.UpdatedDate,
                        DeletedDate = sm.DeletedDate
                    }).ToList() : new List<MenuDTO>()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToMenuPost(string roleId, IEnumerable<Guid> MenuIds)
        {
            if (roleId == null)
            {
                return NotFound();
            }

            await _navigationMenuService.SetPermissionsByRoleIdAsync(roleId, MenuIds);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet("GetMenusForRole/{roleId}")]
        public async Task<JsonResult> GetMenusForRole(string roleId)
        {
            var roleMenus = await _navigationMenuService.GetPermissionsByRoleIdAsync(roleId);
            return Json(new { roleMenus });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMenus()
        {
            var menus = await _menuService.GetMenusAsync();



            var menuDtos = menus.Select(menu => new MenuDTO
            {
                Id = menu.Id,
                Name = menu.Name,
                DisplayOrder = menu.DisplayOrder,
                Icon = menu.Icon,
                ParentId = menu.ParentId,
                IsActive = menu.IsActive
            }).ToList();

            return new JsonResult(new { menus = menuDtos }, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }




        #endregion

    }
}
