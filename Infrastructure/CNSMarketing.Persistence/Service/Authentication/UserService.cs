using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Application.Abstraction.ExternalService;
using CNSMarketing.Application.Abstraction.Service.Manager;
using CNSMarketing.Application.Abstraction.Service.UserRole;
using CNSMarketing.Application.Exceptions.Authentication;
using CNSMarketing.Application.Helpers;
using CNSMarketing.Application.Models.Common;
using CNSMarketing.Application.Models.Requests.Authentication;
using CNSMarketing.Application.Models.Responses.Authentication;
using CNSMarketing.Application.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Repositories.Manager;
using CNSMarketing.Application.Models.DTOs;
using CNSMarketing.Application.Abstraction.Service.SocialMedia;
using CNSMarketing.Application.Const;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Repositories.SocialMedia;
using CNSMarketing.Infrastructure.Enums;

namespace CNSMarketing.Persistence.Service.Authentication
{
    public class UserService : IUserService
    {
        #region Fields & Ctor
        readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ILinkedlnService _linkedlnService;
        private readonly IAPITokenReadRepository _tokenReadRepository;
        //readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, ICustomerService customerService
            , IMailService mailService/*IEndpointReadRepository endpointReadRepository*/ , IConfiguration configuration, ICustomerReadRepository customerReadRepository, ILinkedlnService linkedlnService, IAPITokenReadRepository tokenReadRepository)
        {
            _userManager = userManager;
            _customerService = customerService;
            _mailService = mailService;
            _configuration = configuration;
            _customerReadRepository = customerReadRepository;
            _linkedlnService = linkedlnService;
            _tokenReadRepository = tokenReadRepository;
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
                // Assign the "User" role by default
                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (roleResult.Succeeded)
                {
                    await GetConfirmEmailAsync(user);

                    response.message = "The user has been successfully created and assigned the 'User' role.";

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
                    response.message = "The user was created, but an error occurred during role assignment:";
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

        public async Task GetConfirmEmailAsync(AppUser user)
        {
            // Generate email verification token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Configuration made according to API
            //var apiConfirmationLink = $"{_configuration["AppUrl"]}/api/v1/users/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            // Configuration made according to WEB
            var webConfirmationLink = $"{_configuration["AppUrl"]}/Users/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            // Send verification email
            await _mailService.SendMailAsync(user.Email, "Email Verification", $"Please click on this link to verify your email: \n {webConfirmationLink}");
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
            var userModels = _userManager.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    NameSurname = user.NameSurname,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserName = user.UserName
                })
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var totalCount = await _userManager.Users.CountAsync();

            return new PaginatedList<UserViewModel>(await userModels.ToListAsync(), totalCount, pageIndex, pageSize);



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

        public async Task<UserProfileViewModel> GetUserProfileAsync(AppUser user)
        {
            var info = await GetUser(user);

            LinkedlnUserInfoResponseModel linkedlnProfil = new();
            if (info.AccessToken != null)
                 linkedlnProfil = await _linkedlnService.GetUserInfoAsync(info);

            //    var linkedlnProfil = info.AccessToken != null 
            //? await _linkedlnService.GetUserInfoAsync(info) 
            //: null;

            if (linkedlnProfil == null)
            {
                return new()
                {
                    NameSurName = user.NameSurname,
                    UserName = user.UserName,
                    Linkedln = null // Kullanıcının LinkedIn entegrasyonu yok
                };
            }


            return new()
            {
                NameSurName = user.NameSurname,
                UserName = user.UserName,
                Linkedln = new()
                {
                    UserName = linkedlnProfil.name,
                    FollowCount = null,
                    Url = AppLinkConst.LINKEDIN_URL + "/in/" + linkedlnProfil.given_name + linkedlnProfil.family_name,
                    img = linkedlnProfil.picture,
                }
            };

        }


        #endregion

        #region Private Method 

        private async Task<TokenInfo> GetUser(AppUser user)
        {
            var customer = _customerReadRepository.GetWhere(x => x.UserId == user.Id).FirstOrDefault()!;


            var linkedinToken = await _tokenReadRepository.GetSingleAsync
                (x => x.ApiId == (int)ApiName.Linkedln
                   && x.IsActive == (int)TokenStatus.Active
                   && x.CustomerId == customer.Id);

            if (linkedinToken == null)
            {
                return new TokenInfo
                {
                    CustomerId = 1,
                    UserId = user.Id,
                    UserFullName = user.NameSurname,
                    ExpireDate = user.RefreshTokenEndDate,
                    UserLoginName = user.UserName
                };
            }


            return new TokenInfo
            {
                CustomerId = customer.Id,
                UserId = user.Id,
                UserFullName = user.NameSurname,
                ExpireDate = user.RefreshTokenEndDate,
                UserLoginName = user.UserName,
                AccessToken = linkedinToken.AccessToken,
            };

        }

        #endregion
    }
}
