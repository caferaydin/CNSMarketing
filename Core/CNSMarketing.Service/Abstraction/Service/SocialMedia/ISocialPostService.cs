using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Models.SocialMedia.Model;

namespace CNSMarketing.Application.Abstraction.Service.SocialMedia
{
    public interface ISocialPostService
    {
        Task<bool> CreatePost(CreatePostRequestModel requestModel, TokenInfo tokenInfo);
        Task<bool> CreateLinkedinPost(CreatePostRequestModel requestModel, TokenInfo tokenInfo);
    }
}
