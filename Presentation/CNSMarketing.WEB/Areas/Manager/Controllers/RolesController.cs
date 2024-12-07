using CNSMarketing.Application.Features.Command.Role.CreateRole;
using CNSMarketing.Application.Features.Command.Role.DeleteRole;
using CNSMarketing.Application.Features.Command.Role.UpdateRole;
using CNSMarketing.Application.Features.Queries.Role.GetRoles;
using CNSMarketing.WEB.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IMediator _mediator;

        public RolesController(ILogger<RolesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [DynamicAuthorization("Roles", "AssignRoles")]
        public async Task<IActionResult> AssignRoles([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var request = new GetRolesQueryRequest
            {
                Page = page - 1, // 0 tabanlı indeksleme
                Size = size
            };

            var response = await _mediator.Send(request);


            return View(new GetRolesQueryResponse
            {
                Roles = response.Roles,
                CurrentPage = page,
                PageSize = size,
                TotalCount = response.TotalCount,
                TotalPages = (int)Math.Ceiling(response.TotalCount / (double)size),
                Status = response.Status,
                AvailablePageSizes = new[] { 10, 20, 50, 100 } // Örnek sayfa boyutları
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleCommandRequest createRoleCommandRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(createRoleCommandRequest);
                if (response.IsSuccess)
                {
                    return RedirectToAction("AssignRoles");
                }

            }



            return RedirectToAction("AssignRoles");

        }

        public async Task<IActionResult> UpdateRole(UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            var response = await _mediator.Send(updateRoleCommandRequest);

            if (response.IsSuccess)
            {
                return RedirectToAction("AssignRoles");
            }

            return RedirectToAction("AssignRoles");

        }

        public async Task<IActionResult> DeleteRole(DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            var response = await _mediator.Send(deleteRoleCommandRequest);

            if (response.IsSuccess)
            {
                return RedirectToAction("AssignRoles");
            }

            return RedirectToAction("AssignRoles");

        }

    }
}
