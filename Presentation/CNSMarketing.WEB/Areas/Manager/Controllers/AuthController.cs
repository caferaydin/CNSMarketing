using CNSMarketing.Application.Abstraction.ExternalService.Common;
using CNSMarketing.Application.Features.Command.AppUser.LoginUser;
using CNSMarketing.Application.Features.Command.AppUser.PasswordReset;
using CNSMarketing.Application.Features.Command.AppUser.RefreshTokenLogin;
using CNSMarketing.Application.Features.Command.AppUser.VerifyResetToken;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.WEB.Const;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Areas.Manager.Controllers
{

    [Area("Manager")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IMediator mediator, IMailService mailService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _mailService = mailService;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return Redirect(ConstRoute.UserProfil);

            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserCommandRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                // Başarılı giriş durumunda, kullanıcıyı yönlendir
                return Redirect(ConstRoute.UserProfil);

            }

            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest model)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> PasswordReset(PasswordResetCommandRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                // Şifre sıfırlama başarılıysa bir mesaj gösterebiliriz.
                ViewBag.Message = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyResetToken(VerifyResetTokenCommandRequest model)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                // Token doğrulandıysa şifreyi sıfırlamak için kullanıcıyı yönlendirebiliriz.
                return RedirectToAction("ResetPassword");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(model);
        }

        [HttpGet]
        public IActionResult PasswordReset()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VerifyResetToken()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Logout
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            //Delete cookie
            Response.Cookies.Delete(".AspNetCore.Identity.Application");

            // Redirect to login page or home page
            return Redirect(ConstRoute.HomePage);
        }
    }
}
