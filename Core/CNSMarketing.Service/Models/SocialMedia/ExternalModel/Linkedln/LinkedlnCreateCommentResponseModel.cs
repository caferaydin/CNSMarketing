using Newtonsoft.Json;

namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnCreateCommentResponseModel
    {
        public string actor { get; set; }
        public string agent { get; set; }
        public Created created { get; set; }
        public CommentLastModified lastModified { get; set; }
        public string id { get; set; }

        [JsonProperty("$URN")]
        public string URN { get; set; }
        public CommentMessageResponse message { get; set; }
        public LikesSummaryc likesSummary { get; set; }
        public List<CommentContent> content { get; set; }
        public string @object { get; set; }
    }

    public class CommentContent
    {
        public string type { get; set; }
        public CommentEntity entity { get; set; }
    }

    public class CommentEntity
    {
        public string digitalmediaAsset { get; set; }
    }

    public class CommentLastModified
    {
        public string actor { get; set; }
        public string impersonator { get; set; }
        public long time { get; set; }
    }

    public class LikesSummaryc
    {
        public List<object> selectedLikes { get; set; }
        public int aggregatedTotalLikes { get; set; }
        public bool likedByCurrentUser { get; set; }
        public int totalLikes { get; set; }
    }

    public class CommentMessageResponse
    {
        public List<object> attributes { get; set; }
        public string text { get; set; }
    }
}
