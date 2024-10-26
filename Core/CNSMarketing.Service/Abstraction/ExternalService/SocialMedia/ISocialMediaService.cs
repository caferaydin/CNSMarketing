using CNSMarketing.Service.Models.SocialMedia;
using CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln;

namespace CNSMarketing.Service.Abstraction.ExternalService.SocialMedia
{
    public interface ISocialMediaService
    {
        RedirectResponseModel GetRedirectUrl();
    }
}
