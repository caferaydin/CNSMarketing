using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Domain.Entity.SocialMedia;

public class API : BaseEntity<int>
{
    public string? ApiName { get; set; }
    public bool IsActive { get; set; }
}
