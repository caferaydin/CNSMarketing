using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CNSMarketing.Persistence.Context
{
    public class CNSMarketingDbContextFactory : IDesignTimeDbContextFactory<CNSMarketingDbContext>
    {
        
       
        public CNSMarketingDbContext CreateDbContext(string[] args)
        {
           
            var optionsBuilder = new DbContextOptionsBuilder<CNSMarketingDbContext>();
            //optionsBuilder.UseNpgsql(Configuration.ConnectionString);
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);


            return new CNSMarketingDbContext(optionsBuilder.Options);
        }
    }
}
