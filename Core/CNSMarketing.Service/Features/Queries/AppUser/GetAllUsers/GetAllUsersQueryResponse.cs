using CNSMarketing.Application.Models.ViewModels.User;

namespace CNSMarketing.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public List<UserViewModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<int> AvailablePageSizes { get; set; } // Eklenen özellik
    }
}
