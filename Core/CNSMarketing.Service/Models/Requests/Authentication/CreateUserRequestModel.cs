using System;

namespace CNSMarketing.Application.Models.Requests.Authentication;

public class CreateUserRequestModel
{
    public required string NameSurname { get; set; }
    public required string Username { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirm { get; set; }
}
