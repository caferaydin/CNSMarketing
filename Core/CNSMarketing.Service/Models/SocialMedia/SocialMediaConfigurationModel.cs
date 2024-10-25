namespace CNSMarketing.Service.Models.SocialMedia
{
    public  class SocialMediaConfigurationModel
    {
        public LinkedlnConfigurationModel? LinkedlnModel { get; set; }
        public InstagramConfigurationModel? InstagramModel { get; set; }
        public FacebookConfigurationModel? FacebookModel { get; set; }
    }

    public class LinkedlnConfigurationModel
    {
        public string? AppRedirectUrl { get; set; }
        public string? LinkedinOauthURL { get; set; }
        public string? LinkedinApiURL { get; set; }
        public string? LinkedinClientId { get; set; }
        public string? LinkedinSecretKey { get; set; }
    }

    public class InstagramConfigurationModel
    {
        public string? InstagramRedirectUrl { get; set; }
        public string? AppRedirectUrl { get; set; }
        public string? InstagramAppId { get; set; }
        public string? InstagramSecretKey { get; set; }
        public string? InstagramApiURL { get; set; }
        public string? InstagramScope { get; set; }
        public string? ApiVersion { get; set; }
    }

    public class FacebookConfigurationModel
    {
        public string? FacebookRedirectUrl { get; set; }
        public string? AppRedirectUrl { get; set; }
        public string? FacebookAppId { get; set; }
        public string? FacebookSecretKey { get; set; }
        public string? FacebookApiURL { get; set; }
        public string? FacebookScope { get; set; }
        public string? ApiVersion { get; set; }
    }
}
