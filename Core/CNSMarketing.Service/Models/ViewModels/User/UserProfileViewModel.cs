namespace CNSMarketing.Application.Models.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string? NameSurName { get; set; }
        public string? UserName { get; set; }
        public LinkedlnProfileViewModel? Linkedln { get; set; }
    }

    public class LinkedlnProfileViewModel
    {
        public string? UserName { get; set; }
        public string? FollowCount { get; set; }
        public string? Url { get; set; }
        public string? img { get; set; }
    }

    public class InstagramProfileViewModel
    {
        public string? UserName { get; set; }
        public string? FollowCount { get; set; }
        public string? Url { get; set; }
    }

    public class FacebookProfileViewModel
    {
        public string? UserName { get; set; }
        public string? FollowCount { get; set; }
        public string? Url { get; set; }
    }

}
