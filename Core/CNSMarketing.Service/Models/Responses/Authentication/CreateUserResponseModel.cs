using System;

namespace CNSMarketing.Service.Models.Responses.Authentication;

public class CreateUserResponseModel
{
    public bool success { get; set; }
    public string? message { get; set; }
}
