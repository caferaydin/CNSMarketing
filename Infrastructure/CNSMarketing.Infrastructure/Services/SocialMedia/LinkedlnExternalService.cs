using CNSMarketing.Service.Abstraction.ExternalService;
using CNSMarketing.Service.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Service.Helpers;
using CNSMarketing.Service.Models.SocialMedia;
using CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace CNSMarketing.Infrastructure.Services.SocialMedia
{
    public class LinkedlnExternalService : ILinkedlnExternalService
    {
        private readonly IServiceProvider _serviceProvider;
        string accessTokenUrl = "accessToken";

        LinkedlnConfigurationModel conf;

        Dictionary<string, string> headers = new Dictionary<string, string>();
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        
        public LinkedlnExternalService(IServiceProvider servicesProvider)
        {
            conf = SocialMediaConfigurationHelper.SocialMediaConfigurationModel().LinkedlnModel!;
            _serviceProvider = servicesProvider;

        }

        public RedirectResponseModel GetRedirectUrl()
        {
            var baseUrl = "https://www.linkedin.com/oauth/v2/authorization";
            var responseType = "code";
            var scope = "openid profile r_ads_reporting r_organization_social rw_organization_admin w_member_social r_ads w_organization_social rw_ads r_basicprofile r_organization_admin email r_1st_connections_size";

            var redirectUrl = $"{baseUrl}?response_type={responseType}&client_id={conf.LinkedinClientId}&redirect_uri={Uri.EscapeDataString(conf.AppRedirectUrl)}&scope={Uri.EscapeDataString(scope)}";

            return new RedirectResponseModel
            {
                RedirectUrl = redirectUrl
            };
        }


        public async Task<LinkedlnAccessTokenResponseModel> CreateAccessTokenAsync(LinkedlnAccessTokenRequestModel request)
        {
            
            request.GrantType = "authorization_code";
            request.ClientId = conf.LinkedinClientId;
            request.ClientSecret = conf.LinkedinSecretKey;
            request.RedirectUri = conf.AppRedirectUrl;

            var requestUrl = $"{conf.LinkedinOauthURL}{accessTokenUrl}";

            var keyValuePairs = ApiHelper.ConvertObjectToDictionary(request);
            
            var responseModel = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnAccessTokenResponseModel>>().PostFormEncodedResult(keyValuePairs,requestUrl,headers);

            return responseModel;
        }


    }
}
