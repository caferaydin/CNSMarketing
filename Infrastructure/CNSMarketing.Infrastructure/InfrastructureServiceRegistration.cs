using CNSMarketing.Application.Abstraction.ExternalService;
using CNSMarketing.Application.Abstraction.ExternalService.SocialMedia;
using CNSMarketing.Application.Abstraction.Storage;
using CNSMarketing.Application.Abstraction.Token;
using CNSMarketing.Infrastructure.Enums;
using CNSMarketing.Infrastructure.Services;
using CNSMarketing.Infrastructure.Services.SocialMedia;
using CNSMarketing.Infrastructure.Services.Storage;
using CNSMarketing.Infrastructure.Services.Storage.Azure;
using CNSMarketing.Infrastructure.Services.Storage.Local;
using CNSMarketing.Infrastructure.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CNSMarketing.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServiceRegistration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IStorageService, StorageService>();
            service.AddScoped<ITokenHandler, TokenHandler>();
            service.AddScoped<IMailService, MailService>();


            //service.AddSingleton<IChatGptService>(new ChatGptService(configuration["openAiApiKey"]!));


            service.AddScoped<ILinkedlnExternalService, LinkedlnExternalService>();

            // HttpClient'lar
            service.AddHttpClient("FacebookClient", client =>
            {
                client.BaseAddress = new Uri("https://graph.facebook.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy()) // Polly Retry Policy
            .AddPolicyHandler(GetCircuitBreakerPolicy()); // Polly Circuit Breaker

            service.AddHttpClient("LinkedInClient", client =>
            {
                client.BaseAddress = new Uri("https://api.linkedin.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

            service.AddHttpClient("InstagramClient", client =>
            {
                client.BaseAddress = new Uri("https://graph.instagram.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());


            //service.AddScoped<IServiceManager, ServiceManager>();
            service.AddScoped(typeof(IServiceManager<>), typeof(ServiceManager<>));


        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

    }

}
