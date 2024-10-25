using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Service.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Service.Models.SocialMedia.Model.Linkedln;

namespace CNSMarketing.Service.Abstraction.Service.SocialMedia
{
    public interface ILinkedlnService : ISocialMediaService
    {
        Task<bool> CreateAccessTokenAsync(AccessTokenRequest code, TokenControl tokenControl);
    }
}
