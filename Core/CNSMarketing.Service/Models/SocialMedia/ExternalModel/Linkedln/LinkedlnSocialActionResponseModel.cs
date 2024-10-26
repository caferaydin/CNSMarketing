using Newtonsoft.Json;

namespace CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnSocialActionResponseModel
    {
        public CommentsSummary commentsSummary { get; set; }

        [JsonProperty("$URN")]
        public string URN { get; set; }
        public LikesSummary likesSummary { get; set; }
        public string target { get; set; }
    }

    public class CommentsSummary
    {
        public int totalFirstLevelComments { get; set; }
        public int aggregatedTotalComments { get; set; }
    }

    public class LikesSummary
    {
        public int aggregatedTotalLikes { get; set; }
        public bool likedByCurrentUser { get; set; }
        public int totalLikes { get; set; }
    }
}
