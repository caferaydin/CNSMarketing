using System;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Service.Models.Common;
using CNSMarketing.Service.Models.Requests.Authentication;
using CNSMarketing.Service.Models.Responses.Authentication;
using CNSMarketing.Service.Models.ViewModels.User;

namespace CNSMarketing.Service.Abstraction.Service.UserRole;

public interface IUserService
{
    Task<CreateUserResponseModel> CreateAsync(CreateUserRequestModel model);
    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    Task<PaginatedList<UserViewModel>> GetAllUsersAsync(int pageIndex, int pageSize);
    int TotalUsersCount { get; }
    Task AssignRoleToUserAsnyc(string userId, string[] roles);
    Task<string[]> GetRolesToUserAsync(string userIdOrName);
    Task<bool> IsUsernameUniqueAsync(string username);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
}
