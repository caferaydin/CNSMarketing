using CNSMarketing.Application;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Infrastructure;
using CNSMarketing.Infrastructure.Services.Storage.Azure;
using CNSMarketing.Persistence;
using CNSMarketing.Persistence.Context;
using CNSMarketing.WEB.Exceptions.Middleware;
using CNSMarketing.WEB.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CustomExceptionFilter>(); 
    options.Filters.Add<DynamicAuthorizationAttribute>();
});

builder.Services.AddPersistenceServiceRegistration();
builder.Services.AddInfrastructureServiceRegistration(builder.Configuration);
builder.Services.AddServiceRegistration();


//builder.Services.AddIdentity<AppUser, AppRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 3;
//    options.User.RequireUniqueEmail = false;
//    options.SignIn.RequireConfirmedEmail = false;
//})
//.AddEntityFrameworkStores<CNSMarketingDbContext>()
//.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Manager/Auth/Login";
    options.LogoutPath = "/Manager/Auth/Logout";
    options.AccessDeniedPath = "/Manager/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddHttpContextAccessor();



//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage();   



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseHttpLogging();
app.UseCors();


app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    // Varsayýlan rota
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
