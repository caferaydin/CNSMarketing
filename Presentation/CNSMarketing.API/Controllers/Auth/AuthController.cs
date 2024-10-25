using CNSMarketing.Service.Features.Command;
using CNSMarketing.Service.Features.Command.AppUser.LoginUser;
using CNSMarketing.Service.Features.Command.AppUser.PasswordReset;
using CNSMarketing.Service.Features.Command.AppUser.RefreshTokenLogin;
using CNSMarketing.Service.Features.Command.AppUser.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }

        #region External Login

        //[HttpPost("google-login")]
        //public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        //{
        //    GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        //    return Ok(response);
        //}

        //[HttpPost("facebook-login")]
        //public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        //{
        //    FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
        //    return Ok(response);
        //}

        #endregion

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            BaseCommandResponseModel response = await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }


    }
}
