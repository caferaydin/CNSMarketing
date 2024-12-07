namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnMediaUploadRequestModel
    {
        public RegisterUploadRequest registerUploadRequest { get; set; }

    }

    public class RegisterUploadRequest
    {
        public List<string> recipes { get; set; }
        public string owner { get; set; }
        public List<ServiceRelationship> serviceRelationships { get; set; }
    }

    public class ServiceRelationship
    {
        public string relationshipType { get; set; }
        public string identifier { get; set; }
    }
}
