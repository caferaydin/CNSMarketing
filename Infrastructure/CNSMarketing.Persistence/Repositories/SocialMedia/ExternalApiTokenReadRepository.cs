using CNSMarketing.Domain.Entity.SocialMedia;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Service.Repositories.SocialMedia;

namespace CNSMarketing.Persistence.Repositories.SocialMedia
{
    public class ExternalApiTokenReadRepository : ReadRepository<APIToken, int>, IAPITokenReadRepository
    {
        public ExternalApiTokenReadRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
