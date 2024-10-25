using System;
using Microsoft.AspNetCore.Identity;

namespace CNSMarketing.Domain.Entity.Authentication;

public class AppRole : IdentityRole<string>
{
    public string? Description { get; set; }
}
