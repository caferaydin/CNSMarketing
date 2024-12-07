namespace CNSMarketing.Application.Models.SocialMedia.Model
{
    public class CreatePostRequestModel
    {
        public string ProfileId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public List<MediaObjectModel>? PostMediaData { get; set; }
        public string? PostBoardId { get; set; }
        public List<int> SocialPlatforms { get; set; }

    }
    public class MediaObjectModel
    {
        public string? MediaType { get; set; } //image,video
        public string? MediaBase64 { get; set; } //*asdfasdfasdfasdf
        public string? ContentType { get; set; } //mpt4,jpg,png,jpeg,gif
        public string? CoverImageBase64 { get; set; } //eğer video ise videonun kapagının base64
        public bool? SendToFeed { get; set; }
        public string? MediaUrl { get; set; }
    }
}
