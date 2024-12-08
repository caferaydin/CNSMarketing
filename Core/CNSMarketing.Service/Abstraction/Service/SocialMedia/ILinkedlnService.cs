using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Models.SocialMedia.Model.Linkedln;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.SocialMedia;

namespace CNSMarketing.Application.Abstraction.Service.SocialMedia
{
    public interface ILinkedlnService : ISocialMediaService
    {
        Task<bool> CreateAccessTokenAsync(AccessTokenRequestModel code, TokenInfo tokenInfo);
        Task<LinkedlnSelectAccountResponseModel> GetProfileAsync(TokenInfo tokenInfo);
        Task<LinkedlnCompanyProfilResponseModel> GetCompanyProfileAsync(TokenInfo tokenInfo, string companyUrn);
        Task<List<LinkedlnMediasResponseModel>> GetMediasAsync(TokenInfo tokenInfo, string copanyUrn);
        Task<LinkedlnGetMediaIdResonseModel> GetMediaIdAsync(TokenInfo tokenInfo, string shareUrn);
        Task<LinkedlnCommentResponseModel> GetCommentMediaIdAsync(TokenInfo tokenInfo, string mediaId);
        Task<LinkedlnCommentResponseModel> GetSubCommentAsync(TokenInfo tokenInfo, string mediaId);
        Task<LinkedlnCreateCommentResponseModel> CreateComment(TokenInfo tokenInfo, LinkedlnCreateCommentModel requestModel);
        Task<bool> DeleteComment(TokenInfo tokenInfo, LinkedlnDeleteCommentRequestModel requestModel);
        Task<bool> DeletePost(TokenInfo tokenInfo, string shareUrn);
        Task<LinkedlnUserInfoResponseModel> GetUserInfoAsync(TokenInfo tokenInfo);
        Task<APIToken> TokenIsValid(TokenInfo tokenInfo);


    }
}
