using Newtonsoft.Json;

namespace CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnGetMediaIdResonseModel
    {
        public string author { get; set; }
        public string contentCertificationRecord { get; set; }
        public Created created { get; set; }
        public long firstPublishedAt { get; set; }
        public string id { get; set; }
        public LastModified lastModified { get; set; }
        public string lifecycleState { get; set; }
        public SpecificContent specificContent { get; set; }
        public string versionTag { get; set; }
        public Visibility visibility { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attribute
    {
        public int length { get; set; }
        public int start { get; set; }
        public Values value { get; set; }
    }

    public class ComLinkedinCommonCompanyAttributedEntity
    {
        public string company { get; set; }
    }

    public class ComLinkedinUgcShareContent
    {
        public List<Medium> media { get; set; }
        public ShareCommentary shareCommentary { get; set; }
        public string shareMediaCategory { get; set; }
    }

    public class Medium
    {
        public string media { get; set; }
        public string status { get; set; }
        public List<object> thumbnails { get; set; }
    }

    public class Root
    {
       
    }

    public class ShareCommentary
    {
        public List<Attribute> attributes { get; set; }
        public string text { get; set; }
    }

    public class SpecificContent
    {
        [JsonProperty("com.linkedin.ugc.ShareContent")]
        public ComLinkedinUgcShareContent comlinkedinugcShareContent { get; set; }
    }

    public class Values
    {
        [JsonProperty("com.linkedin.common.CompanyAttributedEntity")]
        public ComLinkedinCommonCompanyAttributedEntity comlinkedincommonCompanyAttributedEntity { get; set; }
    }

    public class Visibility
    {
        [JsonProperty("com.linkedin.ugc.MemberNetworkVisibility")]
        public string comlinkedinugcMemberNetworkVisibility { get; set; }
    }


}
