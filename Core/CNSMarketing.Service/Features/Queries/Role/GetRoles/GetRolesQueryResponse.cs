using CNSMarketing.Service.Models.Responses.Authentication;

namespace CNSMarketing.Service.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryResponse
    {
        public IEnumerable<RoleResponseModel> Roles { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int Status { get; set; }
        public IEnumerable<int> AvailablePageSizes { get; set; }
    }


   
}
