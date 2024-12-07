using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln;
using CNSMarketing.Application.Models.SocialMedia.Model.Linkedln;

namespace CNSMarketing.Application.Abstraction.ExternalService.SocialMedia
{
    public interface ILinkedlnExternalService : ISocialMediaService
    {

        Task<LinkedlnAccessTokenResponseModel> CreateAccessTokenAsync(LinkedlnAccessTokenRequestModel request);
        Task<LinkedlnSelectAccountResponseModel> GetProfileAsync(string accessToken);
        Task<LinkedlnUserInfoResponseModel> GetUserInfoAsync(string accessToken);
        Task<LinkedlnMediaUploadResponseModel> CreateMediaUploadAsync(LinkedlnMediaUploadRequestModel requestModel, string accessToken);
        Task<bool> UploadMediaStatusAsync(LinkedlnUploadMediaStatusRequestModel requestModel);
        Task<LinkedlnCreatePostResponseModel> CreatePost(LinkedlnCreatePostRequestModel requestModel, string accessToken);
        Task<LinkedlnGetMediasResponseModel> GetAllMediasAsync(string accessToken, string companyUrn);
        Task<LinkedlnGetMediaIdResonseModel> GetMediaIdAsync(string accessToken, string shareUrn);
        Task<LinkedlnSocialActionResponseModel> GetMediaIdActions(string accessToken, string shareUrn);
        Task<LinkedlnCommentResponseModel> GetCommentMediaIdAsync(string accessToken, string shareUrn);
        Task<LinkedlnCommentResponseModel> GetSubCommentAsync(string accessToken, string commentId);
        Task<LinkedlnCreateCommentResponseModel> CreateCommentAsync(LinkedlnCreateCommentRequestModel requestModel, string accessToken, string shareUrn);
        Task<bool> DeleteCommentAsync(LinkedlnDeleteCommentRequestModel requestModel);

        Task<bool> DeleteUgcPost(string token, string sharedId);



        // Task<TResponse> GetProfileAsync<TRequest, TResponse>(TRequest request)
        // where TRequest : class
        // where TResponse : class;

        // Task<TResponse> GetAllMediaAsync<TRequest, TResponse>(TRequest request)
        //where TRequest : class
        //where TResponse : class;

        // Task<TResponse> GetMediaByIdAsync<TRequest, TResponse>(TRequest request)
        //  where TRequest : class
        //  where TResponse : class;

        // Task<TResponse> DeleteMediaAsync<TRequest, TResponse>(TRequest request)
        //  where TRequest : class
        //  where TResponse : class;

        // Task<TResponse> GetCommentsAsync<TRequest, TResponse>(TRequest request)
        //  where TRequest : class
        //  where TResponse : class;

        // Task<TResponse> GetCommentByIdAsync<TRequest, TResponse>(TRequest request)
        //  where TRequest : class
        //  where TResponse : class;

        // Task<TResponse> GetSubCommentsAsync<TRequest, TResponse>(TRequest request)
        // where TRequest : class
        // where TResponse : class;

        //Task<TResponse> GetSubCommentByIdAsync<TRequest, TResponse>(TRequest request)
        // where TRequest : class
        // where TResponse : class;

        //Task<TResponse> DeleteCommentByIdAsync<TRequest, TResponse>(TRequest request)
        //where TRequest : class
        //where TResponse : class;

    }
}
