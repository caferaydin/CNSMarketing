using CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln;

namespace CNSMarketing.Service.Abstraction.ExternalService.SocialMedia
{
    public interface ILinkedlnExternalService : ISocialMediaService
    {

        Task<LinkedlnAccessTokenResponseModel> CreateAccessTokenAsync(LinkedlnAccessTokenRequestModel request);

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
