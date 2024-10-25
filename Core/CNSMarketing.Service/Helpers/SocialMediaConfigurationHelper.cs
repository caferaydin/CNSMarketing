using CNSMarketing.Service.Models.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CNSMarketing.Service.Helpers
{
    public static class SocialMediaConfigurationHelper
    {
        public static SocialMediaConfigurationModel SocialMediaConfigurationModel()
        {
            var model = new SocialMediaConfigurationModel()
            {
                LinkedlnModel = new LinkedlnConfigurationModel()
                {
                    AppRedirectUrl = "https://cnsyazilim.com/",
                    LinkedinOauthURL = "https://www.linkedin.com/oauth/v2/",
                    LinkedinApiURL = "https://api.linkedin.com/",
                    LinkedinClientId = "",
                    LinkedinSecretKey = ""
                },
                InstagramModel = new InstagramConfigurationModel()
                {
                    InstagramRedirectUrl = "",
                    AppRedirectUrl = "https://cnsyazilim.com/",
                    InstagramAppId = "",
                    InstagramSecretKey = "",
                    InstagramApiURL = "https://graph.facebook.com",
                    ApiVersion = "v20.0",
                    InstagramScope = "instagram_basic,instagram_content_publish,instagram_manage_comments,instagram_manage_insights,pages_show_list,\t,ads_management,business_management,publish_video,pages_manage_metadata,read_insights,pages_read_user_content,pages_manage_posts,instagram_manage_messages,pages_manage_metadata,pages_messaging,catalog_management,instagram_shopping_tag_products",
                },
                FacebookModel = new FacebookConfigurationModel()
                {
                    FacebookRedirectUrl = "https://www.facebook.com",
                    AppRedirectUrl = "https://cnsyazilim.com/",
                    FacebookAppId = "",
                    FacebookSecretKey = "",
                    FacebookApiURL = "https://graph.facebook.com",
                    ApiVersion = "v20.0",
                    FacebookScope = "email,public_profile,instagram_basic,instagram_content_publish,instagram_manage_comments,instagram_manage_insights,pages_show_list,ads_management,business_management,publish_video,read_insights,pages_read_user_content,pages_manage_posts,instagram_manage_messages,pages_manage_metadata,pages_messaging,catalog_management,instagram_shopping_tag_products,pages_manage_engagement,pages_read_engagement,ads_read,page_events,pages_manage_ads,pages_manage_instant_articles",
                }
            };
            return model;
        }

        
    }
}
