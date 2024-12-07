using CNSMarketing.Application.Features.Command;
using CNSMarketing.Application.Features.Command.AppUser.AssignRoleToUser;
using CNSMarketing.Application.Features.Command.AppUser.CreateUser;
using CNSMarketing.Application.Features.Command.AppUser.UpdatePassword;
using CNSMarketing.Application.Features.Queries.AppUser.GetAllUsers;
using CNSMarketing.Application.Features.Queries.AppUser.GetRolesToUser;
using CNSMarketing.Domain.Entity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.API.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly UserManager<AppUser> _userManager;
        public UsersController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            BaseCommandResponseModel response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Geçersiz kullanıcı.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return Ok("E-posta başarıyla doğrulandı!");

            return BadRequest("E-posta doğrulama işlemi başarısız.");
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            BaseCommandResponseModel response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            BaseCommandResponseModel response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }
    }
}
