using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Service.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Service.Models.SocialMedia.Model.Linkedln;

namespace CNSMarketing.Service.Abstraction.Service.SocialMedia
{
    public interface ILinkedlnService : ISocialMediaService
    {
        Task<bool> CreateAccessTokenAsync(AccessTokenRequestModel code, TokenInfo tokenInfo);
        Task<LinkedlnSelectAccountResponseModel> GetProfileAsync(TokenInfo tokenInfo);
        Task<List<LinkedlnMediasResponseModel>> GetMediasAsync(TokenInfo tokenInfo, string copanyUrn);
        Task<LinkedlnGetMediaIdResonseModel> GetMediaIdAsync(TokenInfo tokenInfo, string shareUrn);
        Task<LinkedlnCommentResponseModel> GetCommentMediaIdAsync(TokenInfo tokenInfo, string mediaId);
        Task<LinkedlnCommentResponseModel> GetSubCommentAsync(TokenInfo tokenInfo, string mediaId);
        Task<LinkedlnCreateCommentResponseModel> CreateComment(TokenInfo tokenInfo, LinkedlnCreateCommentModel requestModel);
        Task<bool> DeleteComment(TokenInfo tokenInfo, LinkedlnDeleteCommentRequestModel requestModel);
        Task<bool> DeletePost(TokenInfo tokenInfo, string shareUrn);
        Task<LinkedlnUserInfoResponseModel> GetUserInfoAsync(TokenInfo tokenInfo);

    }
}
