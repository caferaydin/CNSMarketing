using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Service.Repositories.SocialMedia;

namespace CNSMarketing.Persistence.Repositories.SocialMedia
{
    public class ApiTokenWriteRepository : WriteRepository<APIToken, int>, IAPITokenWriteRepository
    {
        public ApiTokenWriteRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
