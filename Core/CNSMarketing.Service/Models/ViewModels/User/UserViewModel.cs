using System;

namespace CNSMarketing.Service.Models.ViewModels.User;

public class UserViewModel
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? NameSurname { get; set; }
    public string? UserName { get; set; }
    public bool TwoFactorEnabled { get; set; }
}
