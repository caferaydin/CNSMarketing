namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnSelectAccountResponseModel
    {
        public string localizedLastName { get; set; }
        public ProfilePicture profilePicture { get; set; }
        public FirstName firstName { get; set; }
        public string vanityName { get; set; }
        public LastName lastName { get; set; }
        public string localizedHeadline { get; set; }
        public string id { get; set; }
        public Headline headline { get; set; }
        public string localizedFirstName { get; set; }

    }
    public class FirstName
    {
        public Localized localized { get; set; }
        public PreferredLocale preferredLocale { get; set; }
    }

    public class Headline
    {
        public Localized localized { get; set; }
        public PreferredLocale preferredLocale { get; set; }
    }

    public class LastName
    {
        public Localized localized { get; set; }
        public PreferredLocale preferredLocale { get; set; }
    }

    public class Localized
    {
        public string tr_TR { get; set; }
    }

    public class PreferredLocale
    {
        public string country { get; set; }
        public string language { get; set; }
    }

    public class ProfilePicture
    {
        public string displayImage { get; set; }
    }
}
