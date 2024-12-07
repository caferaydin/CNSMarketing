using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Infrastructure.Services.SocialMedia;
using CNSMarketing.Application.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Application.Abstraction.Service.SocialMedia;
using CNSMarketing.Application.Helpers;
using CNSMarketing.Application.Models.SocialMedia;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Models.SocialMedia.Model.Linkedln;
using CNSMarketing.Application.Repositories.SocialMedia;

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

        public async Task<bool> CreateAccessTokenAsync(AccessTokenRequestModel request, TokenInfo tokenControl)
        {
            var response = await _externalService.CreateAccessTokenAsync(new() { Code = request.code });

            if (response.access_token.Length < 10)
                return false;

            var existingToken = await _tokenReadRepository.GetSingleAsync(x => x.CustomerId == tokenControl.CustomerId && x.ApiId == (int)ApiName.Linkedln && x.IsActive == (int)TokenStatus.Active);

            if (existingToken != null)
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

        public async Task<LinkedlnSelectAccountResponseModel> GetProfileAsync(TokenInfo tokenInfo)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                return await _externalService.GetProfileAsync(linkedlnToken.AccessToken!);
            }

            return new();

        }

        public async Task<LinkedlnUserInfoResponseModel> GetUserInfoAsync(TokenInfo tokenInfo)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                return await _externalService.GetUserInfoAsync(linkedlnToken.AccessToken!);
            }

            return new();

        }
       
        public async Task<List<LinkedlnMediasResponseModel>> GetMediasAsync(TokenInfo tokenInfo, string companyUrn)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                var getAllPost = await _externalService.GetAllMediasAsync(linkedlnToken.AccessToken, companyUrn);

                var filteredPosts = new List<LinkedlnMediasResponseModel>();

                foreach (var element in getAllPost.elements)
                {
                    var getallSocialResponseModel = new LinkedlnMediasResponseModel
                    {
                        Id = element.id,
                        text = element.text?.text,
                        owner = element.owner,
                        createdDate = Helpers.ConvertTimeStampToDateTime(element.lastModified.time),
                        sharedUrn = element.activity,
                    };

                    var socialActionTask = await _externalService.GetMediaIdActions(element.activity, linkedlnToken.AccessToken);

                    getallSocialResponseModel.SocialActionReponseModel = new SocialActionResponseModel
                    {
                        aggregatedTotalComments = socialActionTask.commentsSummary?.aggregatedTotalComments ?? 0,
                        aggregatedTotalLikes = socialActionTask.likesSummary?.aggregatedTotalLikes ?? 0
                    };

                    var mediaList = new List<GetAllPostImage>();

                    if (element.content != null)
                    {
                        foreach (var images in element.content?.contentEntities)
                        {
                            mediaList.Add(new GetAllPostImage()
                            {
                                mediaUrl = images.entityLocation
                            });
                        }
                        getallSocialResponseModel.GetAllPostImages = mediaList;
                    }

                    filteredPosts.Add(getallSocialResponseModel);
                }

                return filteredPosts;

            }

            return new();
        }

        public async Task<LinkedlnGetMediaIdResonseModel> GetMediaIdAsync(TokenInfo tokenInfo, string shareUrn)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                return await _externalService.GetMediaIdAsync(linkedlnToken.AccessToken!, shareUrn);
            }

            return new();
        }

        public async Task<bool> DeletePost(TokenInfo tokenInfo, string shareUrn)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                return await _externalService.DeleteUgcPost(linkedlnToken.AccessToken!, shareUrn);

            }
            return false;
        }

        public async Task<LinkedlnCommentResponseModel> GetCommentMediaIdAsync(TokenInfo tokenInfo, string mediaId)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                return await _externalService.GetCommentMediaIdAsync(linkedlnToken.AccessToken!, mediaId);

            }
            return new(); 
        }

        public async Task<LinkedlnCommentResponseModel> GetSubCommentAsync(TokenInfo tokenInfo, string mediaId)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                return await _externalService.GetSubCommentAsync(linkedlnToken.AccessToken!, mediaId);

            }
            return new(); 
        }

        public async Task<LinkedlnCreateCommentResponseModel> CreateComment(TokenInfo tokenInfo, LinkedlnCreateCommentModel requestModel)
        {
            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {

                var createComment = new LinkedlnCreateCommentRequestModel
                {
                    Actor = "urn:li:company:" + requestModel.actor,
                    ObjectProperty = "urn:li:activity:" + requestModel.sharedUrn,
                    Message = new CommentMessage
                    {
                        Text = requestModel.text
                    }
                };

                return await _externalService.CreateCommentAsync(createComment,linkedlnToken.AccessToken, requestModel.Id);

            }
            return new();
        }

        public async Task<bool> DeleteComment(TokenInfo tokenInfo, LinkedlnDeleteCommentRequestModel requestModel)
        {

            var linkedlnToken = await TokenIsValid(tokenInfo);

            if (linkedlnToken.AccessToken != null)
            {
                requestModel.token = linkedlnToken.AccessToken;
                requestModel.sharedUrn = "urn:li:share:" + requestModel.sharedUrn;
                requestModel.actor = "urn:li:company:" + requestModel.actor;
                var result = await _externalService.DeleteCommentAsync(requestModel);

                return result;
            }

            return false;
        }

        private async Task<APIToken> TokenIsValid(TokenInfo tokenInfo)
        {
            var linkedinToken = await _tokenReadRepository.GetSingleAsync
                (x => x.ApiId == (int)ApiName.Linkedln
                   && x.IsActive == (int)TokenStatus.Active
                   && x.CustomerId == tokenInfo.CustomerId);

            if (linkedinToken != null)
                return linkedinToken;

            return null;
        }

      
    }
}
