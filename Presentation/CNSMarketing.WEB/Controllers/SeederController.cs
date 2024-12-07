using CNSMarketing.Application.Abstraction.Service.Manager;
using CNSMarketing.Application.Abstraction.Service.UserRole;
using CNSMarketing.Application.Models.DTOs.Menus;
using CNSMarketing.Application.Repositories.SocialMedia;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Persistence.Service.Authentication;
using CNSMarketing.Persistence.Service.Manager;
using CNSMarketing.WEB.Const;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Controllers
{
    public class SeederController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;
        private readonly IRoleService _roleService;
        private readonly INavigationMenuService _navigationMenuService;
        private readonly IAPIWriteRepository _apiWriteRepository;



        public SeederController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMenuService menuService, IRoleService roleService, IUserService userService, INavigationMenuService navigationMenuService, IAPIWriteRepository apiWriteRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _menuService = menuService;
            _roleService = roleService;
            _userService = userService;
            _navigationMenuService = navigationMenuService;
            _apiWriteRepository = apiWriteRepository;
        }

        public async Task<IActionResult> Index()
        {

            var menu = _menuService.GetAll();

            if (menu.Count() > 0)
                return Redirect(ConstRoute.UserProfil);
           
            await CreateMenus();
            await UserData();
            await CreateApi();

            //Logout UserData Token
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            //Delete cookie
            Response.Cookies.Delete(".AspNetCore.Identity.Application");

            return Redirect(ConstRoute.HomePage);
        }


        #region User Role Data 

        private async Task UserData()
        {
            await CreateRole();
            await CreateUser();
            await UserRoleAssign();
            await RoleToMenuAssign();

        }

        private async Task CreateUser()
        {
            await _userService.CreateAsync(new()
            {
                Email = "zng.caferaydin@gmail.com",
                NameSurname = "Cafer AYDIN",
                Password = "123456ada",
                PasswordConfirm = "123456ada",
                PhoneNumber = "5421092933",
                Username = "caferaydin"
            });

        }

        private async Task CreateRole()
        {
            await _roleService.CreateRole("Admin", "Admin");
            await _roleService.CreateRole("User", "User");


        }

        private async Task UserRoleAssign()
        {
            var users = await _userService.GetAllUsersAsync(1, 10);

            foreach (var item in users)
            {
                await _userService.AssignRoleToUserAsnyc(item.Id, ["Admin"]);
            }

        }

        private async Task RoleToMenuAssign()
        {
            //var menus =   _menuService.GetAll().AsEnumerable().Select(x => x.Id);
            var menus = await _menuService.GetMenusAsync();
            var menuIds = menus.Select(x => x.Id).ToList();


            //var query = _roleManager.Roles;

            //IQueryable<AppRole> rolesQuery = null;

            //rolesQuery = query;

            //var roles = rolesQuery
            //    .Select(r => new RoleDto { Id = r.Id, Name = r.Name, Description = r.Description })
            //.FirstOrDefault();


            //var roleId = roles?.Id;

            //await _navigationMenuService.SetPermissionsByRoleIdAsync(roleId, menuIds);

            var role = _roleManager.Roles.FirstOrDefault(x => x.Name == "Admin");

            await _navigationMenuService.SetPermissionsByRoleIdAsync(role!.Id, menuIds);

        }

        #endregion

        #region Menus
        private async Task CreateMenus()
        {


            var menus = new List<Menu>();

            // Dashboard DisplayOrder - 1
            var dashboardMenu = new Menu
            {
                Name = "Dashboard",
                ControllerName = "Dashboard",
                ActionName = "Index",
                AreaName = "Manager",
                DisplayOrder = 1,
                Permitted = false,
                Icon = "fe fe-home",
                IsActive = true
            };
            menus.Add(dashboardMenu);


            var socialMedia = new Menu()
            {
                Name = "SocialMedia",
                ControllerName = "",
                ActionName = "",
                AreaName = "Manager",
                DisplayOrder = 2,
                Permitted = false,
                Icon = "fa fa-share",
                IsActive = true,
                SubMenus = new List<Menu>
                {
                    new Menu()
                    {
                        Name = "Linkedln",
                        ControllerName = "Linkedln",
                        ActionName = "Index",
                        AreaName = "SocialMedia",
                        DisplayOrder = 1,
                        Permitted = false,
                        Icon = "",
                        IsActive = true,
                    }
                   
                }
            };

            menus.Add(socialMedia);


            var managerMenu = new Menu()
            {
                Name = "User And Role Management",
                ControllerName = "Management",
                ActionName = "",
                AreaName = "",
                DisplayOrder = 5,
                Permitted = false,
                Icon = "fe fe-seedling",
                IsActive = true,

                SubMenus = new List<Menu>()
                {
                    new Menu()
                     {
                         Name = "Users",
                         ControllerName = "User",
                         ActionName = "",
                         AreaName = "",
                         DisplayOrder = 1,
                         Permitted = false,
                         Icon = "fa fa-cogs",
                        IsActive = true,

                         SubMenus = new List<Menu>()
                         {
                              new Menu()
                              {
                                  Name = "User List",
                                  ControllerName = "Users",
                                  ActionName = "GetAllUsers",
                                  AreaName = "",
                                  DisplayOrder = 1,
                                  Permitted = false,
                                  Icon = "fa fa-seedling",
                                  IsActive = true,

                              }
                         }
                     },

                    new Menu()
                    {
                         Name = "Roles",
                         ControllerName = "Roles",
                         ActionName = "",
                         AreaName = "",
                         DisplayOrder = 2,
                         Permitted = false,
                         Icon = "fa fa-seedling",
                         IsActive = true,
                         SubMenus = new List<Menu>()
                         {
                            new Menu()
                            {
                                Name = "Role List",
                                ControllerName = "Roles",
                                ActionName = "AssignRoles",
                                AreaName = "Manager",
                                DisplayOrder = 1,
                                Permitted = false,
                                Icon = "fa fa-seedling",
                                IsActive = true
                            },
                            new Menu()
                            {
                                Name = "User To Role Assign",
                                ControllerName = "Users",
                                ActionName = "AssignRoleToUser",
                                AreaName = "",
                                DisplayOrder = 2,
                                Permitted = false,
                                Icon = "fa fa-seedling",
                                IsActive = true,
                            },


                         }
                    },

                     new Menu()
                    {
                         Name = "Menu Management",
                         ControllerName = "MenuManagement",
                         ActionName = "",
                         AreaName = "",
                         DisplayOrder = 2,
                         Permitted = false,
                         Icon = "fa fa-seedling",
                         IsActive = true,
                         SubMenus = new List<Menu>()
                         {
                            new Menu()
                            {
                                Name = "Menus",
                                ControllerName = "MenuManagement",
                                ActionName = "Index",
                                AreaName = "Manager",
                                DisplayOrder = 1,
                                Permitted = false,
                                Icon = "fa fa-seedling",
                                IsActive = true
                            },
                            new Menu()
                            {
                                Name = "Menu to Role Assign",
                                ControllerName = "MenuManagement",
                                ActionName = "AssignRoleToMenu",
                                AreaName = "Manager",
                                DisplayOrder = 2,
                                Permitted = false,
                                Icon = "fa fa-seedling",
                                IsActive = true,
                            },


                         }
                    }
                }
            };


        
            menus.Add(managerMenu);



            await _menuService.AddRangeAsync(menus);

        }

        #endregion


        #region SocialMedia 
        private async Task CreateApi()
        {
            var api = new API()
            {
                ApiName = "Linkedln",
                IsActive = true,
            };

            await _apiWriteRepository.AddAsync(api);
            await _apiWriteRepository.SaveAsync();

        }

        #endregion


    }
}
