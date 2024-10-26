using Newtonsoft.Json;

namespace CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnCommentResponseModel
    {
        public Pagingc paging { get; set; }
        public List<Elementc> elements { get; set; }
    }

    public class Createdc
    {
        public string actor { get; set; }
        public string impersonator { get; set; }
        public object time { get; set; }
    }

    public class Elementc
    {
        public string actor { get; set; }
        public Createdc created { get; set; }
        public LastModifiedc lastModified { get; set; }
        public string id { get; set; }

        [JsonProperty("$URN")]
        public string URN { get; set; }
        public Message message { get; set; }
        public string @object { get; set; }
    }

    public class LastModifiedc
    {
        public string actor { get; set; }
        public string impersonator { get; set; }
        public object time { get; set; }
    }

    public class Message
    {
        public List<object> attributes { get; set; }
        public string text { get; set; }
    }

    public class Pagingc
    {
        public int start { get; set; }
        public int count { get; set; }
        public List<object> links { get; set; }
        public int total { get; set; }
    }



}
