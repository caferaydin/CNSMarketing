using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Service.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Service.Abstraction.Service.SocialMedia;
using CNSMarketing.Service.Abstraction.Token;
using CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Service.Models.SocialMedia.Model.Linkedln;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers.SocialMedia
{
    [Route("api/v1/SocialMedia/[controller]")]
    [ApiController]
    public class LinkedlnController : BaseController
    {
        private readonly ILinkedlnService _linkedlnService;

        public LinkedlnController(ITokenHandler tokenHandler, ILinkedlnService linkedlnService)
         : base(tokenHandler) // BaseController'a tokenHandler'ı geçiriyoruz
        {
            _linkedlnService = linkedlnService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
            {
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));
            }
            return Ok(GenericResponse(_linkedlnService.GetRedirectUrl()));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateToken([FromBody] AccessTokenRequest requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));
            
            return Ok(GenericResponse(await _linkedlnService.CreateAccessTokenAsync(requestModel, tokenControl)));
        }



    }
}
