namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnCompanyProfilResponseModel
    {
        public string vanityName { get; set; }
        public Website website { get; set; }
        public string localizedName { get; set; }
        public FoundedOn foundedOn { get; set; }
        public Created created { get; set; }
        public List<object> groups { get; set; }
        public Description description { get; set; }
        public string versionTag { get; set; }
        public CoverPhotoV2 coverPhotoV2 { get; set; }
        public DefaultLocale defaultLocale { get; set; }
        public string organizationType { get; set; }
        public List<object> alternativeNames { get; set; }
        public List<object> specialties { get; set; }
        public List<object> localizedSpecialties { get; set; }
        public Name name { get; set; }
        public string primaryOrganizationType { get; set; }
        public List<Location> locations { get; set; }
        public LastModified lastModified { get; set; }
        public int id { get; set; }
        public string localizedDescription { get; set; }
        public bool autoCreated { get; set; }
        public string localizedWebsite { get; set; }
        public LogoV2 logoV2 { get; set; }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string geographicArea { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
    }

    public class CoverPhotoV2
    {
        public string cropped { get; set; }
        public string original { get; set; }
        public CropInfo cropInfo { get; set; }
    }

    public class CropInfo
    {
        public int x { get; set; }
        public int width { get; set; }
        public int y { get; set; }
        public int height { get; set; }
    }

    public class DefaultLocale
    {
        public string country { get; set; }
        public string language { get; set; }
    }

    public class Description
    {
        public Localized localized { get; set; }
        public PreferredLocale preferredLocale { get; set; }
    }

    public class FoundedOn
    {
        public int year { get; set; }
    }


    public class Location
    {
        public Description description { get; set; }
        public string locationType { get; set; }
        public Address address { get; set; }
        public string localizedDescription { get; set; }
        public string streetAddressFieldState { get; set; }
        public string geoLocation { get; set; }
    }

    public class LogoV2
    {
        public string cropped { get; set; }
        public string original { get; set; }
        public CropInfo cropInfo { get; set; }
    }

    public class Name
    {
        public Localized localized { get; set; }
        public PreferredLocale preferredLocale { get; set; }
    }

   

    public class Website
    {
        public Localized localized { get; set; }
        public PreferredLocale preferredLocale { get; set; }
    }



}
