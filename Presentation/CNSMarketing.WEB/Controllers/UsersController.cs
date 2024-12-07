using CNSMarketing.Application.Abstraction.ExternalService;
using CNSMarketing.Application.Abstraction.Service.UserRole;
using CNSMarketing.Application.Features.Command.AppUser.AssignRoleToUser;
using CNSMarketing.Application.Features.Command.AppUser.CreateUser;
using CNSMarketing.Application.Features.Command.AppUser.UpdatePassword;
using CNSMarketing.Application.Features.Queries.AppUser.GetAllUsers;
using CNSMarketing.Application.Features.Queries.AppUser.GetRolesToUser;
using CNSMarketing.Application.Features.Queries.Role.GetRoles;
using CNSMarketing.Application.Helpers;
using CNSMarketing.Application.Models.ViewModels.User;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.WEB.Const;
using CNSMarketing.WEB.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Controllers
{
    [DynamicAuthorization]
    public class UsersController : Controller
    {
        #region Fields & Ctor
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IMediator mediator, IMailService mailService, IUserService userService, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _mailService = mailService;
            _userService = userService;
            _userManager = userManager;
        }
        #endregion

        #region Methods

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(ConstRoute.UserProfil);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            bool isUsernameUnique = await _userService.IsUsernameUniqueAsync(createUserCommandRequest.Username);
            bool isEmailUnique = await _userService.IsEmailUniqueAsync(createUserCommandRequest.Email);
            bool isPhoneNumberUnique = await _userService.IsPhoneNumberUniqueAsync(createUserCommandRequest.PhoneNumber);

            if (!isUsernameUnique)
            {
                ModelState.AddModelError("Username", "Kullanıcı adı zaten kullanımda.");
            }
            if (!isEmailUnique)
            {
                ModelState.AddModelError("Email", "E-posta adresi zaten kullanımda.");
            }
            if (!isPhoneNumberUnique)
            {
                ModelState.AddModelError("PhoneNumber", "Telefon numarası zaten kullanımda.");
            }

            if (ModelState.IsValid)
            {
                // Komutu işleyin
                var response = await _mediator.Send(createUserCommandRequest);

                if (response.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }

                // İşlem başarısızsa, hata mesajını ekleyin
                ModelState.AddModelError(string.Empty, response.Message);
            }

            // Model doğrulama veya işlem başarısızsa kayıt formunu tekrar göster
            return View("Register", createUserCommandRequest);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            var response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }


        [HttpGet] // TODO1
        [Authorize]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Geçersiz kullanıcı.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["message"] = "E-posta başarıyla doğrulandı!";
                return Redirect(ConstRoute.UserProfil);
            }

            return BadRequest("E-posta doğrulama işlemi başarısız.");
        }

        [HttpGet("get-confirm-email")] // TODO1
        public async Task<IActionResult> GetConfirmEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            if (user.EmailConfirmed)
            {
                return Json(new { success = false, emailConfirmed = true, message = "E-posta zaten onaylanmış." });
            }

            // Kullanıcının son tıklama süresini kontrol edin (örneğin, bir CustomClaim veya başka bir veri kaynağı kullanarak)
            var lastSentTime = user.LastEmailSentTime; // Varsayım: Bu alanı eklediniz.
            if (lastSentTime != null && (DateTime.UtcNow - lastSentTime.Value).TotalSeconds < 60)
            {
                return Json(new { success = false, emailConfirmed = false, message = "E-postayı tekrar göndermek için 1 dakika bekleyin." });
            }

            // Email gönderim işlemi
            await _userService.GetConfirmEmailAsync(user);

            // Son gönderim zamanını güncelleyin
            user.LastEmailSentTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return Json(new { success = true, emailConfirmed = false, message = "Doğrulama e-postası gönderildi." });
        }



        [DynamicAuthorization] // TODO1
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            var response = await _mediator.Send(getAllUsersQueryRequest);

            return View(response);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            var profile = await _userService.GetUserProfileAsync(user);

            return View(profile);
        }

        public async Task<IActionResult> AssignRoleToUser()
        {

            // Eğer model geçerli değilse tekrar formu yükle
            var users = await _mediator.Send(new GetAllUsersQueryRequest());

            //var roles = await _mediator.Send(new GetRolesQueryRequest());

            var model = new AssignRoleViewModel
            {
                Users = users.Users.Select(x => new AppUser
                {
                    Id = x.Id,
                    UserName = x.UserName
                }).ToList(),

                //Roles = roles.Roles.Select(x => new AppRole
                //{
                //    Name = x.Name
                //}).ToList(),
            };



            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest model)
        {

            if (ModelState.IsValid)
            {
                var request = new AssignRoleToUserCommandRequest
                {
                    UserId = model.UserId,
                    Roles = model.Roles
                };

                var response = await _mediator.Send(model);

                TempData["SuccessMessage"] = "Rol başarılı bir şekilde atandı.";

                return Redirect(Request.Headers["Referer"].ToString());

            }

            TempData["Message"] = "Bir Problem Oluştu ";
            TempData["MessageType"] = "error";
            return Redirect(Request.Headers["Referer"].ToString());


            //return RedirectToAction("AssignRoleToUser");
            //}

            //var response = await _mediator.Send(assignRoleToUserCommandRequest);
            //return Ok(response);
        }



        [HttpGet("GetRolesToUser/{userId}")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] string userId)
        {
            var response = await _mediator.Send(new GetRolesToUserQueryRequest { UserId = userId });
            return Ok(new { response.UserRoles });
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _mediator.Send(new GetRolesQueryRequest());


            return Ok(response);
        }
        #endregion


        #region Check

        [HttpGet]
        public async Task<IActionResult> checkUsername(string username)
        {
            bool isUnique = await _userService.IsUsernameUniqueAsync(username);
            return Ok(isUnique);
        }

        [HttpGet]
        public async Task<IActionResult> checkEmail(string email)
        {
            bool isUnique = await _userService.IsEmailUniqueAsync(email);
            return Ok(isUnique);
        }

        [HttpGet]
        public async Task<IActionResult> checkPhone(string phone)
        {
            string cleanedPhone = ApplicationHelpers.CleanPhoneNumber(phone);

            bool isUnique = await _userService.IsPhoneNumberUniqueAsync(cleanedPhone);
            return Ok(isUnique);
        }

        #endregion
    }
}
