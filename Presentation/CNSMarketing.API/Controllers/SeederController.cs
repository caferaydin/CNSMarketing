using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Persistence.Service.Authentication;
using CNSMarketing.Service.Abstraction.Service.UserRole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeederController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public SeederController(UserManager<AppUser> userManager, IRoleService roleService, IUserService userService)
        {
            _userManager = userManager;
            _roleService = roleService;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> UserData()
        {
            var users = _userManager.Users.ToList();

            if(users.Count > 0)
                return BadRequest();

            await Seeder.CreateRole(_roleService);
            await Seeder.CreateUser(_userService);
            await Seeder.UserRoleAssign(_userService);

            return Ok("Success");

            
        }
        
    }

    public static class Seeder
    {
        public static async Task<bool> CreateUser(IUserService userService)
        {

            //await userService.CreateAsync(new()
            //{
            //    Email = "zng.caferaydin@gmail.com",
            //    NameSurname = "Cafer AYDIN",
            //    PhoneNumber = "5421092933",
            //    UserName = "ccaferaydin",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //});



            //await _userManager.CreateAsync(user, "123456ada");

            await userService.CreateAsync(new()
            {
                Email = "zng.caferaydin@gmail.com",
                NameSurname = "Cafer AYDIN",
                Password = "caferaydin123",
                PasswordConfirm = "caferaydin123",
                PhoneNumber = "5421092933",
                Username = "caferaydin"
            });

            return true;
        }

        public static async Task<bool> CreateRole(IRoleService _roleService)
        {
            await _roleService.CreateRole("Admin", "Admin");
            await _roleService.CreateRole("User", "User");


            return true;
        }

        public static async Task<bool> UserRoleAssign(IUserService _userService)
        {
            var users = await _userService.GetAllUsersAsync(1, 10);

            foreach (var item in users)
            {
                await _userService.AssignRoleToUserAsnyc(item.Id, ["Admin"]);

            }

            return true;
        }
    }
}
