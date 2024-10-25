using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Service.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Service.Abstraction.Service.SocialMedia;
using CNSMarketing.Service.Helpers;
using CNSMarketing.Service.Models.SocialMedia;
using CNSMarketing.Service.Models.SocialMedia.Model.Linkedln;
using CNSMarketing.Service.Repositories.SocialMedia;

namespace CNSMarketing.Persistence.Service.SocialMedia
{
    public class LinkedlnService : ILinkedlnService
    {
        private readonly ILinkedlnExternalService _externalService;
        private readonly IAPITokenWriteRepository _tokenWriteRepository;
        private readonly IAPITokenReadRepository _tokenReadRepository;

        public LinkedlnService(ILinkedlnExternalService externalService, IAPITokenWriteRepository tokenWriteRepository, IAPITokenReadRepository tokenReadRepository)
        {
            _externalService = externalService;
            _tokenWriteRepository = tokenWriteRepository;
            _tokenReadRepository = tokenReadRepository;
        }

        public RedirectResponseModel GetRedirectUrl()
        {
           return _externalService.GetRedirectUrl();
        }

        public async Task<bool> CreateAccessTokenAsync(AccessTokenRequest request, TokenControl tokenControl)
        {
            var response = await _externalService.CreateAccessTokenAsync(new() { Code = request.code});

            if (response.access_token.Length < 10)
                return false;

            var existingToken = await _tokenReadRepository.GetSingleAsync(x => x.CustomerId == tokenControl.CustomerId && x.ApiId == (int)ApiName.Linkedln && x.IsActive == (int)TokenStatus.Active);

            if(existingToken != null)
            {
                await _tokenWriteRepository.RemoveAsync(existingToken.Id);
                await _tokenWriteRepository.SaveAsync();
            }


            var createModel = new APIToken
            {
                CustomerId = tokenControl.CustomerId, 
                ApiId = (int)ApiName.Linkedln,
                AccessToken = response.access_token,
                RefreshToken = response.refresh_token,
                ResponseJson = Helpers.ConvertToJson(response),
                ExpireDate = Helpers.ConvertSecondsToDate(response.expires_in),
                IsActive = (int)TokenStatus.Active,

            };

            await _tokenWriteRepository.AddAsync(createModel);
            await _tokenWriteRepository.SaveAsync();

            return true;
        }
    }
}
