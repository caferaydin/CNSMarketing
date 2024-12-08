using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Application.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Application.Abstraction.Service.SocialMedia;
using CNSMarketing.Application.Abstraction.Token;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Models.SocialMedia.Model.Linkedln;
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
        public async Task<IActionResult> CreateToken([FromBody] AccessTokenRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));
            
            return Ok(GenericResponse(await _linkedlnService.CreateAccessTokenAsync(requestModel, tokenInfo)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfile()
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            return Ok(GenericResponse(await _linkedlnService.GetProfileAsync(tokenInfo)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserInfo()
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            return Ok(GenericResponse(await _linkedlnService.GetUserInfoAsync(tokenInfo)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetCompanyProfile([FromBody] LinkedlnBaseRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            return Ok(GenericResponse(await _linkedlnService.GetCompanyProfileAsync(tokenInfo, requestModel.urn)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllMedia([FromBody] LinkedlnBaseRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.urn == null)
                return BadRequest($"{requestModel.urn}");
            return Ok(GenericResponse(await _linkedlnService.GetMediasAsync(tokenInfo, requestModel.urn)));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetMediaId([FromBody] LinkedlnBaseRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.urn == null)
                return BadRequest($"{requestModel.urn}");
            return Ok(GenericResponse(await _linkedlnService.GetMediaIdAsync(tokenInfo, requestModel.urn)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetCommentMediaId([FromBody] LinkedlnBaseRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.urn == null)
                return BadRequest($"{requestModel.urn}");
            return Ok(GenericResponse(await _linkedlnService.GetCommentMediaIdAsync(tokenInfo, requestModel.urn)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetSubComment([FromBody] LinkedlnBaseRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.urn == null)
                return BadRequest($"{requestModel.urn}");
            return Ok(GenericResponse(await _linkedlnService.GetSubCommentAsync(tokenInfo, requestModel.urn)));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateComment([FromBody] LinkedlnCreateCommentModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.sharedUrn == null)
                return BadRequest($"{requestModel.sharedUrn}");
            return Ok(GenericResponse(await _linkedlnService.CreateComment(tokenInfo, requestModel)));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteMediaId([FromBody] LinkedlnBaseRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.urn == null)
                return BadRequest($"{requestModel.urn}");

            return Ok(GenericResponse(await _linkedlnService.DeletePost(tokenInfo, requestModel.urn)));
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteCommentId([FromBody] LinkedlnDeleteCommentRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            if (requestModel.sharedUrn == null)
                return BadRequest($"{requestModel.sharedUrn}");

            return Ok(GenericResponse(await _linkedlnService.DeleteComment(tokenInfo, requestModel)));
        }


    }
}
