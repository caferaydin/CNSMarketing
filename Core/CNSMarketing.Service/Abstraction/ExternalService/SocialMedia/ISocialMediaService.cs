using CNSMarketing.Application.Models.SocialMedia;

namespace CNSMarketing.Application.Abstraction.ExternalService.SocialMedia
{
    public interface ISocialMediaService
    {
        RedirectResponseModel GetRedirectUrl();
    }
}
