using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Persistence.Service;
using CNSMarketing.Persistence.Service.Authentication;
using CNSMarketing.Persistence.Service.Manager;
using CNSMarketing.Persistence.Service.SocialMedia;
using CNSMarketing.Service.Abstraction;
using CNSMarketing.Service.Abstraction.ExternalService;
using CNSMarketing.Service.Abstraction.Service.Authentication;
using CNSMarketing.Service.Abstraction.Service.Manager;
using CNSMarketing.Service.Abstraction.Service.SocialMedia;
using CNSMarketing.Service.Abstraction.Service.UserRole;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CNSMarketing.Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceServiceRegistration(this IServiceCollection services)
    {
        services.AddDbContext<CNSMarketingDbContext>(option => option.UseSqlServer(Configuration.ConnectionString));
        //services.AddDbContext<CNSMarketingDbContext>(option => option.UseNpgsql(Configuration.ConnectionString));


        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<CNSMarketingDbContext>()
            .AddDefaultTokenProviders();


        /////////////////// SOCIAL MEDÝA  ///////////////////
        services.AddScoped<ILinkedlnService, LinkedlnService>();
        services.AddScoped<ISocialPostService, SocialPostService>();
        /////////////////// SOCIAL MEDÝA END  ///////////////////


        services.AddScoped<ICustomerService, CustomerService>();



        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IInternalAuthentication, AuthService>();
        services.AddScoped<IRoleService, RoleService>();


        #region Start Repository & Service Reflection


        #region  Repository 

        var applicationAssembly = Assembly.Load("CNSMarketing.Service");
        var persistenceAssembly = Assembly.Load("CNSMarketing.Persistence");


        var repositoryInterfaces = applicationAssembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository")).ToList();

        var repositoryClasses = persistenceAssembly.GetTypes()
            .Where(t => t.IsClass && t.Name.EndsWith("Repository")).ToList();

        foreach (var repositoryInterface in repositoryInterfaces)
        {
            var repositoryClass = repositoryClasses.FirstOrDefault(c => c.GetInterfaces().Contains(repositoryInterface));
            if (repositoryClass != null)
            {
                services.AddScoped(repositoryInterface, repositoryClass);
            }
        }
        #endregion

        #region Service 

        //var serviceInterfaces = applicationAssembly.GetTypes()
        //    .Where(t => t.IsInterface && t.Name.EndsWith("Service")).ToList();

        //var serviceClasses = persistenceAssembly.GetTypes()
        //    .Where(t => t.IsClass && t.Name.EndsWith("Service")).ToList();

        //foreach (var serviceInterface in serviceInterfaces)
        //{
        //    var serviceClass = serviceClasses.FirstOrDefault(c => c.GetInterfaces().Contains(serviceInterface));
        //    if (serviceClass != null)
        //    {
        //        services.AddScoped(serviceInterface, serviceClass);
        //    }
        //}

        #endregion

        #endregion End Repository & Service Reflection
    }
}
