using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Service.Models.SocialMedia.Model;

namespace CNSMarketing.Service.Abstraction.Service.SocialMedia
{
    public interface ISocialPostService
    {
        Task<bool> CreatePost(CreatePostRequestModel requestModel, TokenInfo tokenInfo);
        Task<bool> CreateLinkedinPost(CreatePostRequestModel requestModel, TokenInfo tokenInfo);
    }
}
