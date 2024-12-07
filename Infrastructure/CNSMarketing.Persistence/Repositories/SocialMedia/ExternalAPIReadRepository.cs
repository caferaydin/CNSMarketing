using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Application.Repositories.SocialMedia;

namespace CNSMarketing.Persistence.Repositories.SocialMedia
{
    public class ExternalAPIReadRepository : ReadRepository<API, int>, IAPIReadRepository
    {
        public ExternalAPIReadRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
