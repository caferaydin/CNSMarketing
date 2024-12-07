using Azure.Core;
using CNSMarketing.Application.Abstraction.ExternalService;
using CNSMarketing.Application.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Application.Helpers;
using CNSMarketing.Application.Models.DTOs;
using CNSMarketing.Application.Models.SocialMedia;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Models.SocialMedia.Model.Linkedln;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;

namespace CNSMarketing.Infrastructure.Services.SocialMedia
{
    public class LinkedlnExternalService : ILinkedlnExternalService
    {
        private readonly IServiceProvider _serviceProvider;

        LinkedlnConfigurationModel conf;

        Dictionary<string, string> headers = new Dictionary<string, string>();
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();


        #region String
        string accessTokenUrl = "accessToken";
        string getProfile = "v2/me";
        string getUserInfo = "v2/userinfo";
        string registerUpload = "v2/assets?action=registerUpload";
        string createUgcPsot = "v2/ugcPosts";
        string getAllMedias = "v2/shares?q=owners&owners=urn:li:organization:";
        string getMediaId = "v2/ugcPosts/urn%3Ali%3Ashare%3A";
        string getSocialAction = "v2/socialActions/";
        string getComment = "/comments";
        string createComment = "v2/socialActions/urn:li:share:";
        string deletePosts = "v2/shares/urn%3Ali%3Ashare%3A";

        #endregion

        public LinkedlnExternalService(IServiceProvider servicesProvider)
        {
            conf = SocialMediaConfigurationHelper.SocialMediaConfigurationModel().LinkedlnModel!;
            _serviceProvider = servicesProvider;

        }

        public RedirectResponseModel GetRedirectUrl()
        {
            var baseUrl = "https://www.linkedin.com/oauth/v2/authorization";
            var responseType = "code";
            //var scope = "openid profile r_ads_reporting r_organization_social rw_organization_admin w_member_social r_ads w_organization_social rw_ads r_basicprofile r_organization_admin email r_1st_connections_size r_member_social w_compliance";
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
            
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnAccessTokenResponseModel>>().PostFormEncodedResult(keyValuePairs, requestUrl, headers);

            return result;
        }

        public async Task<LinkedlnSelectAccountResponseModel> GetProfileAsync(string accessToken)
        {
            var requestUrl = conf.LinkedinApiURL + getProfile;
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnSelectAccountResponseModel>>().GetAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnUserInfoResponseModel> GetUserInfoAsync(string accessToken)
        {
            var requestUrl = conf.LinkedinApiURL + getUserInfo;
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnUserInfoResponseModel>>().GetAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnMediaUploadResponseModel> CreateMediaUploadAsync(LinkedlnMediaUploadRequestModel requestModel, string accessToken)
        {
            var requestUrl = conf.LinkedinApiURL + registerUpload;
            headers.Add("X-Restli-Protocol-Version", "2.0.0");
            headers.Add("Authorization", $"Bearer {accessToken}");

            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnMediaUploadResponseModel>>().PostAsync(requestUrl, requestModel, headers);
            return result;
        }

        public async Task<bool> UploadMediaStatusAsync(LinkedlnUploadMediaStatusRequestModel requestModel)
        {
            headers.Clear();
            //headers.Add("X-Restli-Protocol-Version", "2.0.0");
            headers.Add("Authorization", $"Bearer {requestModel.Token}");
            var requestData = Helpers.ConvertFromBase64ToByte(requestModel.MediaBase64);
            var result = await _serviceProvider.GetRequiredService<IServiceManager<bool>>().PostBinaryAsync(requestModel.PostUrl, requestData, headers);
            return result;
        }

        public async Task<LinkedlnCreatePostResponseModel> CreatePost(LinkedlnCreatePostRequestModel requestModel, string accessToken)
        {
            headers.Clear();
            var requestUrl = conf.LinkedinApiURL + createUgcPsot;
            headers.Add("X-Restli-Protocol-Version", "2.0.0");
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnCreatePostResponseModel>>().PostAsync(requestUrl, requestModel, headers);
            return result;
        }

        public async Task<bool> DeleteUgcPost(string token, string sharedId)
        {
            var requestUrl = conf.LinkedinApiURL + deletePosts + sharedId;
            headers.Clear();
            headers.Add("Authorization", $"Bearer {token}");
            headers.Add("X-Restli-Protocol-Version", "2.0.0");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnCreatePostResponseModel>>().DeleteAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnGetMediasResponseModel> GetAllMediasAsync(string accessToken, string companyUrn)
        {
            var requestUrl = conf.LinkedinApiURL + getAllMedias + companyUrn;
            //headers.Add("X-Restli-Protocol-Version", "2.0.0");
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnGetMediasResponseModel>>().GetAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnGetMediaIdResonseModel> GetMediaIdAsync(string accessToken, string shareUrn)
        {
            var requestUrl = conf.LinkedinApiURL + getMediaId + shareUrn;
            headers.Clear();
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnGetMediaIdResonseModel>>().GetAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnSocialActionResponseModel> GetMediaIdActions(string accessToken, string shareUrn)
        {
            var requestUrl = conf.LinkedinApiURL + getMediaId + shareUrn;
            headers.Clear();
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnSocialActionResponseModel>>().GetAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnCommentResponseModel> GetCommentMediaIdAsync(string accessToken, string shareUrn)
        {
            var requestUrl = conf.LinkedinApiURL + getSocialAction + "urn:li:share:"+ shareUrn + getComment; // urn%3Ali%3Ashare%3A
            headers.Clear();
            headers.Add("Authorization", $"Bearer {accessToken}");
            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnCommentResponseModel>>().GetAsync(requestUrl, headers);
            return result;
        }

        public async Task<LinkedlnCommentResponseModel> GetSubCommentAsync(string accessToken, string commentId)
        {
            var sharedUrn = Uri.EscapeDataString(commentId);

            var requestUrl = conf.LinkedinApiURL + getSocialAction + sharedUrn + getComment; // urn%3Ali%3Ashare%3A
            headers.Clear();
            headers.Add("Authorization", $"Bearer {accessToken}");

            var result = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnCommentResponseModel>>().GetAsync(requestUrl, headers);

            return result;
        }

        public async Task<LinkedlnCreateCommentResponseModel> CreateCommentAsync(LinkedlnCreateCommentRequestModel requestModel, string accessToken, string shareUrn)
        {
            headers.Clear();
            var requestUrl = conf.LinkedinApiURL + createComment + shareUrn + getComment;
            headers.Add("Authorization", $"Bearer {accessToken}");
            var responseModel = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnCreateCommentResponseModel>>().PostAsync(requestUrl, requestModel, headers);
            return responseModel;
        }

        public async Task<bool> DeleteCommentAsync(LinkedlnDeleteCommentRequestModel requestModel)
        {
            headers.Clear();
            var requestUrl = conf.LinkedinApiURL + getSocialAction + requestModel.sharedUrn + "/comments/" + requestModel.commentId + "?actor=" + requestModel.actor;
            headers.Add("Authorization", $"Bearer {requestModel.token}");

            var responseModel = await _serviceProvider.GetRequiredService<IServiceManager<LinkedlnCommentResponseModel>>().DeleteAsync(requestUrl, headers);

            return responseModel;
        }
    }
}
