namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnGetMediasResponseModel
    {
        public Paging paging { get; set; }
        public List<Element> elements { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Content
    {
        public List<ContentEntity> contentEntities { get; set; }
        public string description { get; set; }
        public string shareMediaCategory { get; set; }
    }

    public class ContentEntity
    {
        public string description { get; set; }
        public string entityLocation { get; set; }
        public List<Thumbnail> thumbnails { get; set; }
        public string entity { get; set; }
    }

    public class Created
    {
        public string actor { get; set; }
        public long time { get; set; }
    }

    public class Distribution
    {
        public LinkedInDistributionTarget linkedInDistributionTarget { get; set; }
    }

    public class Element
    {
        public string owner { get; set; }
        public string activity { get; set; }
        public bool edited { get; set; }
        public Created created { get; set; }
        public Text text { get; set; }
        public LastModified lastModified { get; set; }
        public string id { get; set; }
        public Distribution distribution { get; set; }
        public Content content { get; set; }
    }

    public class ImageSpecificContent
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class LastModified
    {
        public string actor { get; set; }
        public long time { get; set; }
    }

    public class Link
    {
        public string type { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class LinkedInDistributionTarget
    {
        public bool visibleToGuest { get; set; }
    }

    public class Paging
    {
        public int start { get; set; }
        public int count { get; set; }
        public List<Link> links { get; set; }
        public int total { get; set; }
    }


    public class Text
    {
        public string text { get; set; }
    }

    public class Thumbnail
    {
        public ImageSpecificContent imageSpecificContent { get; set; }
        public string resolvedUrl { get; set; }
    }
}
