using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.Common;

namespace CNSMarketing.Domain.Entity.Manager;

public class Customer : BaseEntity<int>
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CompanyName { get; set; }
    public string? MobilePhone { get; set; }
    public string? LocalPhone { get; set; }
    public int CustomerTypeId { get; set; }
    public byte ÝsActive { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
}
