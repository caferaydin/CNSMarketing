using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Application.Abstraction.Service.SocialMedia;
using CNSMarketing.Application.Abstraction.Token;
using CNSMarketing.Application.Models.SocialMedia.Model;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers.SocialMedia
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SocialMediaController : BaseController
    {
        private readonly ISocialPostService _postService;

        public SocialMediaController(ITokenHandler tokenHandler, ISocialPostService postService) 
            : base(tokenHandler)
        {
            _postService = postService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestModel requestModel)
        {
            var tokenValidationResult = await TokenControl(HttpContext);

            if (tokenValidationResult != TokenResult.Ok)
                return BadRequest(GenericResponse(GetInvalidTokenResponse()));

            return Ok(GenericResponse(await _postService.CreatePost(requestModel, tokenInfo)));
        }

    }
}
