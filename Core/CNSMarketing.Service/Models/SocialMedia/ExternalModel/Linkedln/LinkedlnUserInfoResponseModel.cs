namespace CNSMarketing.Service.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnUserInfoResponseModel
    {
        public string name { get; set; }
        public string sub { get; set; }
        public Locale locale { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }

    }

    public class Locale
    {
        public string country { get; set; }
        public string language { get; set; }
    }
}
