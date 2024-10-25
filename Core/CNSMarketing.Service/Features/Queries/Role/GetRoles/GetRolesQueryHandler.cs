using CNSMarketing.Service.Abstraction.Service.UserRole;
using CNSMarketing.Service.Models.Responses.Authentication;
using MediatR;

namespace CNSMarketing.Service.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        readonly IRoleService _roleService;
        //test
        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _roleService.GetAllRoles(request.Page, request.Size);

            // TotalPages hesaplaması
            var totalPages = (int)Math.Ceiling((double)count / request.Size);

            return new GetRolesQueryResponse
            {
                Roles = datas.Select(r => new RoleResponseModel { Id = r.Id, Name = r.Name, Description = r.Description }),
                CurrentPage = request.Page,
                PageSize = request.Size,
                TotalCount = count,
                TotalPages = totalPages,
                Status = 200, // İsteğin başarılı olduğunu varsayıyoruz
                AvailablePageSizes = new List<int> { 10, 20, 50, 100 } // Örnek page sizes
            };
        }
    }
}
