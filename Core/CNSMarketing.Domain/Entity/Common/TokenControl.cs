using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNSMarketing.Domain.Entity.Common
{
    public class TokenControl
    {
        public string? UserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime ExpireDate { get; set; }
        public string? UserFullName { get; set; }
        public string? UserLoginName { get; set; }
        public string? AccessToken { get; set; }
    }
}
