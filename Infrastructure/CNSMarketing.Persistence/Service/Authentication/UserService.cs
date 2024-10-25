using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Service.Abstraction.Service.Manager;
using CNSMarketing.Service.Abstraction.Service.UserRole;
using CNSMarketing.Service.Exceptions.Authentication;
using CNSMarketing.Service.Helpers;
using CNSMarketing.Service.Models.Common;
using CNSMarketing.Service.Models.Requests.Authentication;
using CNSMarketing.Service.Models.Responses.Authentication;
using CNSMarketing.Service.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CNSMarketing.Persistence.Service.Authentication
{
    public class UserService : IUserService
    {
        #region Fields & Ctor
        readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        //readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, ICustomerService customerService
            /*IEndpointReadRepository endpointReadRepository*/ )
        {
            _userManager = userManager;
            _customerService = customerService;
            //_endpointReadRepository = endpointReadRepository;
        }

        #endregion

        #region Methods
        //public async Task<CreateUserResponse> CreateAsync(CreateUserRequest model)
        //{
        //    IdentityResult result = await _userManager.CreateAsync(new()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = model.Username,
        //        PhoneNumber = model.PhoneNumber,
        //        Email = model.Email,
        //        NameSurname = model.NameSurname,
        //    }, model.Password);



        //    CreateUserResponse response = new() { Succeeded = result.Succeeded };

        //    if (result.Succeeded)
        //        response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
        //    else
        //        foreach (var error in result.Errors)
        //            response.Message += $"{error.Code} - {error.Description}\n";

        //    return response;
        //}

        public async Task<CreateUserResponseModel> CreateAsync(CreateUserRequestModel model)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                NameSurname = model.NameSurname,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            CreateUserResponseModel response = new() { success = result.Succeeded };

            if (result.Succeeded)
            {
                // Varsayılan olarak "User" rolünü ata
                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");



                if (roleResult.Succeeded)
                {
                    response.message = "Kullanıcı başarıyla oluşturulmuştur ve 'User' rolü atanmıştır.";

                    await _customerService.AddAsync(new()
                    {
                        UserId = user.Id,
                        CustomerName = user.NameSurname,
                        CustomerEmail = user.Email,
                        MobilePhone = user.PhoneNumber,
                        LocalPhone = user.PhoneNumber,
                        İsActive = 1,
                    });

                }
                else
                {
                    response.message = "Kullanıcı oluşturuldu, ancak rol atama sırasında bir hata oluştu: ";
                    foreach (var error in roleResult.Errors)
                    {
                        response.message += $"{error.Code} - {error.Description}\n";
                    }
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.message += $"{error.Code} - {error.Description}\n";
                }
            }

            return response;
        }


        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }
        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }
        public async Task<PaginatedList<UserViewModel>> GetAllUsersAsync(int pageIndex, int pageSize)
        {
            //var users = _userManager.Users.Where(x => x.EmailConfirmed == true).AsQueryable();
            var users = _userManager.Users.AsQueryable();
            var userModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurname,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName
            }).AsQueryable();

            return await PaginatedList<UserViewModel>.CreateAsync(userModels, pageIndex, pageSize);


        }

        public int TotalUsersCount => _userManager.Users.Count();
        public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }
        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { };
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }

        public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return user == null;
        }




        //public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        //{
        //    var userRoles = await GetRolesToUserAsync(name);

        //    if (!userRoles.Any())
        //        return false;

        //    Endpoint? endpoint = await _endpointReadRepository.Table
        //             .Include(e => e.Roles)
        //             .FirstOrDefaultAsync(e => e.Code == code);

        //    if (endpoint == null)
        //        return false;

        //    var hasRole = false;
        //    var endpointRoles = endpoint.Roles.Select(r => r.Name);

        //    foreach (var userRole in userRoles)
        //    {
        //        foreach (var endpointRole in endpointRoles)
        //            if (userRole == endpointRole)
        //                return true;
        //    }

        //    return false;
        //}

        #endregion
    }
}
