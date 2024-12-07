using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNSMarketing.Application.Abstraction.ExternalService.SocialMedia
{
    public interface IMessenger<Response, Request>
        where Response : class
        where Request : class
    {


    }
}
