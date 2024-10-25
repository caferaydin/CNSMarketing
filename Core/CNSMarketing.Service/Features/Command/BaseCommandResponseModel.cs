using System;

namespace CNSMarketing.Service.Features.Command;

public class BaseCommandResponseModel
{
    public bool success { get; set; }
    public string message { get; set; }

}
