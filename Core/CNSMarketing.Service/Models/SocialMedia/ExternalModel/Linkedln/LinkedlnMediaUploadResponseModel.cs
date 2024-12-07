using Newtonsoft.Json;

namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnMediaUploadResponseModel
    {
        public Value value { get; set; }
    }

    public class ComLinkedinDigitalmediaUploadingMediaUploadHttpRequest
    {
        public string uploadUrl { get; set; }
        public Headers headers { get; set; }
    }

    public class Headers
    {
        [JsonProperty("media-type-family")]
        public string mediatypefamily { get; set; }
    }

    public class UploadMechanism
    {
        [JsonProperty("com.linkedin.digitalmedia.uploading.MediaUploadHttpRequest")]
        public ComLinkedinDigitalmediaUploadingMediaUploadHttpRequest comlinkedindigitalmediauploadingMediaUploadHttpRequest { get; set; }
    }

    public class Value
    {
        public string mediaArtifact { get; set; }
        public UploadMechanism uploadMechanism { get; set; }
        public string asset { get; set; }
        public string assetRealTimeTopic { get; set; }
    }
}
