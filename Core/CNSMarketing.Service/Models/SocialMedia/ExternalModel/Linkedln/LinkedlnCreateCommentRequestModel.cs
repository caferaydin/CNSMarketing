using Newtonsoft.Json;

namespace CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnCreateCommentRequestModel
    {
        [JsonProperty("actor")]
        public string? Actor { get; set; }

        [JsonProperty("object")]
        public string? ObjectProperty { get; set; }

        [JsonProperty("message")]
        public CommentMessage? Message { get; set; }

        //[JsonProperty("content")]
        //public List<Contents>? Contents { get; set; }
    }

    public class Contents
    {
        [JsonProperty("entity")]
        public Entity? Entity { get; set; }
    }
    public class Entity
    {
        [JsonProperty("image")]
        public string? image { get; set; }
    }

    public class CommentMessage
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
    }

}
