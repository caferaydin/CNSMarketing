using System;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Application.Models.Common;
using CNSMarketing.Application.Models.Requests.Authentication;
using CNSMarketing.Application.Models.Responses.Authentication;
using CNSMarketing.Application.Models.ViewModels.User;

namespace CNSMarketing.Application.Abstraction.Service.UserRole;

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
    Task GetConfirmEmailAsync(AppUser user);


    Task<UserProfileViewModel> GetUserProfileAsync(AppUser user);
}
