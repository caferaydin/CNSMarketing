using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Service.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Service.Abstraction.Service.SocialMedia;
using CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Service.Models.SocialMedia.Model;
using CNSMarketing.Service.Repositories.SocialMedia;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CNSMarketing.Persistence.Service.SocialMedia
{
    public class SocialPostService : ISocialPostService
    {
        private readonly IAPITokenReadRepository _tokenReadRepository;
        private readonly ILinkedlnExternalService _linkedlnExternalService;

        public SocialPostService(IAPITokenReadRepository tokenReadRepository, ILinkedlnExternalService linkedlnExternalService)
        {
            _tokenReadRepository = tokenReadRepository;
            _linkedlnExternalService = linkedlnExternalService;
        }

        public async Task<bool> CreatePost(CreatePostRequestModel requestModel, TokenInfo tokenInfo)
        {
            if (requestModel.SocialPlatforms.Contains((int)ApiName.Linkedln))
                await CreateLinkedinPost(requestModel, tokenInfo);

            return true;
        }

        public async Task<bool> CreateLinkedinPost(CreatePostRequestModel requestModel, TokenInfo tokenInfo)
        {

            var linkedinToken = await _tokenReadRepository.GetSingleAsync
                (x => x.ApiId == (int)ApiName.Linkedln
                   && x.IsActive == (int)TokenStatus.Active
                   && x.CustomerId == tokenInfo.CustomerId);

            // Determine the recipe based on whether media is image or video
            var isImage = !string.IsNullOrEmpty(requestModel.PostMediaData[0].CoverImageBase64);
            var recipe = isImage
                ? "urn:li:digitalmediaRecipe:feedshare-image"
                : "urn:li:digitalmediaRecipe:feedshare-video";

            // asset
            var registerUploadRequestModel = new LinkedlnMediaUploadRequestModel()
            {
                registerUploadRequest = new RegisterUploadRequest()
                {
                    recipes = new List<string> { recipe },
                    owner = requestModel.ProfileId,
                    serviceRelationships = new List<ServiceRelationship>()
            {
                new ServiceRelationship()
                {
                    relationshipType = "OWNER",
                    identifier = "urn:li:userGeneratedContent"
                }
                    }
                }
            };

            var registerResponsModel = await _linkedlnExternalService.CreateMediaUploadAsync(registerUploadRequestModel, linkedinToken.AccessToken);

            var mediaType = "";
            // post
            var mediaList = new List<MediaPost>();
            if (isImage || requestModel.PostMediaData != null)
            {
                var mediaBase64 = isImage ? requestModel.PostMediaData[0].CoverImageBase64 : requestModel.PostMediaData[0].MediaBase64;
                mediaType = isImage ? "IMAGE" : "VIDEO";

                var media = new MediaPost()
                {
                    status = "READY",
                    description = new DescriptionPost() { text = requestModel.PostTitle },
                    media = registerResponsModel.value.asset,
                    title = new DescriptionPost() { text = requestModel.PostTitle },
                    //originalUrl = "https://blog.linkedin.com/"
                };
                mediaList.Add(media);

                // media upload
                var mediaUploadRequestModel = new LinkedlnUploadMediaStatusRequestModel()
                {
                    PostUrl = registerResponsModel.value.uploadMechanism.comlinkedindigitalmediauploadingMediaUploadHttpRequest.uploadUrl,
                    MediaBase64 = mediaBase64,
                    Token = linkedinToken.AccessToken
                };

                var responseMediaUpload = await _linkedlnExternalService.UploadMediaStatusAsync(mediaUploadRequestModel);
            }

            var ugcRequest = new LinkedlnCreatePostRequestModel()
            {
                author = requestModel.ProfileId,
                lifecycleState = "PUBLISHED",
                specificContent = new SpecificContentPost() //com.linkedin.ugc.ShareContent
                {
                    comlinkedinugcShareContent = new ComLinkedinUgcShareContentPost()
                    {
                        shareCommentary = new ShareCommentaryPost()
                        {

                            text = requestModel.PostContent,
                        },
                        shareMediaCategory = mediaType != "" ? mediaType : "NONE",
                        media = mediaList
                    }
                },
                visibility = new VisibilityPost()
                {
                    comlinkedinugcMemberNetworkVisibility = "PUBLIC"
                }
            };

            
            Console.WriteLine(JsonConvert.SerializeObject(ugcRequest));

            var responseUgcPost = await _linkedlnExternalService.CreatePost(ugcRequest, linkedinToken.AccessToken);

            if (!string.IsNullOrWhiteSpace(responseUgcPost.id))
                return true;
            return false;
        }

       
    }
}
