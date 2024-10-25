using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Domain.Entity.SocialMedia;

public class APIToken : BaseEntity<int>
{
    public int CustomerId { get; set; }
    public int ApiId { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? ResponseJson { get; set; }
    public string? UserAccessToken { get; set; }
    public string? PageAccessToken { get; set; }
    public string? ApiUserId { get; set; }
    public string? ApiPageId { get; set; }
    public int IsActive { get; set; }


}
