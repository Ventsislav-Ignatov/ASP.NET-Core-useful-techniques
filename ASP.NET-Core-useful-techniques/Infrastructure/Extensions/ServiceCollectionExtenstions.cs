namespace ASP.NET_Core_useful_techniques.Infrastructure.Extensions
{
    using ASP.NET.Core.Useful.Techniques.Common.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class ServiceCollectionExtenstions
    {
        private static Type transientServiceInterfaceType = typeof(IService);
        private static Type singletonServiceInterfaceType = typeof(ISingletonService);
        private static Type scopedServiceInterfaceType = typeof(IScopedService);

        internal static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Assembly
               .GetAssembly(transientServiceInterfaceType)
               .GetTypes()
               .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}") && !t.IsAbstract && !t.IsGenericType)
               .ToList()
               .ForEach(x => services.AddService(x));

            return services;
        }

        private static Type Interface(this Type type) => type.GetInterface($"I{type.Name}");

        private static void AddService(this IServiceCollection services, Type currentService)
        {
            if (transientServiceInterfaceType.IsAssignableFrom(currentService))
            {
                services.AddTransient(currentService.Interface(), currentService);
            }
            else if (singletonServiceInterfaceType.IsAssignableFrom(currentService))
            {
                services.AddSingleton(currentService.Interface(), currentService);
            }
            else if (scopedServiceInterfaceType.IsAssignableFrom(currentService))
            {
                services.AddScoped(currentService.Interface(), currentService);
            }
        }
    }
}
