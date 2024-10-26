using Newtonsoft.Json;

namespace CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnCreatePostRequestModel
    {
        public string author { get; set; }
        public string lifecycleState { get; set; }
        public SpecificContentPost specificContent { get; set; }
        public VisibilityPost visibility { get; set; }
    }


    public class ComLinkedinUgcShareContentPost
    {
        public ShareCommentaryPost shareCommentary { get; set; }
        public string shareMediaCategory { get; set; }
        public List<MediaPost> media { get; set; }
    }

    public class DescriptionPost
    {
        public string text { get; set; }
    }

    public class MediaPost
    {
        public string? media { get; set; }
        public string status { get; set; }
        public DescriptionPost title { get; set; }
        public DescriptionPost description { get; set; }
        //public string originalUrl { get; set; }
    }


    public class ShareCommentaryPost
    {
        public string text { get; set; }
    }

    public class SpecificContentPost
    {
        [JsonProperty("com.linkedin.ugc.ShareContent")]
        public ComLinkedinUgcShareContentPost comlinkedinugcShareContent { get; set; }
    }

    public class VisibilityPost
    {
        [JsonProperty("com.linkedin.ugc.MemberNetworkVisibility")]
        public string comlinkedinugcMemberNetworkVisibility { get; set; }
    }
}
