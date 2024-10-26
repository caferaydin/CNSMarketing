namespace CNSMarketing.Domain.Entity.Common
{
    public class TokenInfo
    {
        public string? UserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? UserFullName { get; set; }
        public string? UserLoginName { get; set; }
        public string? AccessToken { get; set; }
    }
}
