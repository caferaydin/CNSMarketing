using Microsoft.AspNetCore.Identity;

namespace CNSMarketing.Domain.Entity.Authentication;

public class AppUser : IdentityUser<string>
{
    public  string? NameSurname { get; set; }
    public  string? RefreshToken { get; set; }
    public DateTime RefreshTokenEndDate { get; set; }
    
}
