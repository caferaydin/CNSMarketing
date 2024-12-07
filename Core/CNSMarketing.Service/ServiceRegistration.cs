using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CNSMarketing.Application
{
    public static class ServiceRegistration
    {

        public static void AddServiceRegistration(this IServiceCollection collection)
        {
            collection.AddMediatR(Assembly.GetExecutingAssembly());
            collection.AddHttpClient();


            var config = TypeAdapterConfig.GlobalSettings; // Küresel Mapster ayarları
            collection.AddSingleton(config);
            collection.AddScoped<IMapper, ServiceMapper>(); // Mapster'ın IMapper arayüzü
        }
    }
}
