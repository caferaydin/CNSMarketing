using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Application.Repositories.SocialMedia;

namespace CNSMarketing.Persistence.Repositories.SocialMedia
{
    public class APIWriteRepository : WriteRepository<API, int>, IAPIWriteRepository
    {
        public APIWriteRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
