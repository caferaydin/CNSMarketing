namespace CNSMarketing.Service.Models.SocialMedia.Model.Linkedln
{
    public class LinkedlnMediasResponseModel
    {
        public string Id { get; set; }
        public string? sharedUrn { get; set; }
        public string? owner { get; set; }
        public string? text { get; set; }
        public DateTime? createdDate { get; set; }
        public List<GetAllPostImage>? GetAllPostImages { get; set; }
        public SocialActionResponseModel SocialActionReponseModel { get; set; }
    }

    public class SocialActionResponseModel
    {
        public int? aggregatedTotalComments { get; set; }
        public int? aggregatedTotalLikes { get; set; }
    }

    public class GetAllPostImage
    {
        public string? mediaUrl { get; set; }
    }
}
