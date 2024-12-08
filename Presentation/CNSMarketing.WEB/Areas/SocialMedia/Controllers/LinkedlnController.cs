using CNSMarketing.Application.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Application.Abstraction.Service.Manager;
using CNSMarketing.Application.Abstraction.Service.SocialMedia;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Models.SocialMedia.Model.Linkedln;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CNSMarketing.WEB.Areas.SocialMedia.Controllers
{
    [Area("SocialMedia")]
    public class LinkedlnController : Controller
    {
        private readonly ILinkedlnService _linkedlnService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;

        public LinkedlnController(ILinkedlnService linkedlnService, UserManager<AppUser> userManager, ICustomerService customerService)
        {
            _linkedlnService = linkedlnService;
            _userManager = userManager;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }





        #region  Json Controller
        [Authorize]
        [HttpGet]
        public IActionResult GetRedirectUrl()
        {

            var redirectUrl = _linkedlnService.GetRedirectUrl();


            return Ok(new { url = redirectUrl.RedirectUrl });

        }


        //[HttpPost]
        //public async Task<IActionResult> ProcessCode([FromBody] AccessTokenRequestModel request)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var customer = await _customerService.GetWhere(x => x.UserId == user.Id).FirstOrDefaultAsync();

        //    var tokenInfo = new TokenInfo()
        //    {
        //        CustomerId = customer.Id,
        //        UserId = user.Id,
        //        UserFullName = user.NameSurname,
        //        UserLoginName = user.NameSurname
        //    };

        //    var tokenResponse = await _linkedlnService.CreateAccessTokenAsync(new() { code = request.code}, tokenInfo);


        //    return Ok(new { message = "LinkedIn bağlantısı başarılı." });
        //}


        [HttpPost]
        public async Task<IActionResult> ProcessCode([FromBody] AccessTokenRequestModel request)
        {
            try
            {
                // Şu anki kullanıcıyı al
                var user = await _userManager.GetUserAsync(User);

                // Kullanıcı bilgisine göre müşteri verisini al
                var customer = await _customerService.GetWhere(x => x.UserId == user.Id).FirstOrDefaultAsync();

                if (customer == null)
                {
                    return BadRequest("Kullanıcıya ait müşteri bilgisi bulunamadı.");
                }

                // Token bilgilerini oluştur
                var tokenInfo = new TokenInfo()
                {
                    CustomerId = customer.Id,
                    UserId = user.Id,
                    UserFullName = user.NameSurname,
                    UserLoginName = user.NameSurname
                };

                // LinkedIn token'ını almak için servis metodunu çağır
                var tokenResponse = await _linkedlnService.CreateAccessTokenAsync(new() { code = request.code }, tokenInfo);

                // TokenResponse kontrolü
                if (tokenResponse == null)
                {
                    return BadRequest("LinkedIn'den token alınamadı.");
                }

                // Token alındıysa başarılı bir şekilde sonuç dön
                TempData["message"] = "LinkedIn bağlantısı başarılı.";

                return Redirect(Request.Headers["Referer"].ToString());

            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapabilir veya kullanıcıya uygun hata mesajı dönebilirsiniz.
                return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }


        #endregion


    }
}
