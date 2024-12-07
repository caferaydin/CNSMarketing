namespace CNSMarketing.Application.Models.SocialMedia.Model.Linkedln
{
    public class LinkedlnDeleteCommentRequestModel
    {
        public string sharedUrn { get; set; }
        public string commentId { get; set; }
        public string? token { get; set; }
        public string actor { get; set; }
    }
}
