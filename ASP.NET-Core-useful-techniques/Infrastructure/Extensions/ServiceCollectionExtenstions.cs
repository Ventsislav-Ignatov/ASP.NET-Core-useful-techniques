namespace ASP.NET_Core_useful_techniques.Infrastructure.Extensions
{
    using ASP.NET.Core.Useful.Techniques.Common.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtenstions
    {
        static ServiceCollectionExtenstions()
        {
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var transientServiceInterfaceType = typeof(IService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);

            var exportedTypes = transientServiceInterfaceType.Assembly.GetExportedTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}") && !t.IsAbstract && !t.IsGenericType)
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Interface != null)
                .ToList();

            foreach (var service in exportedTypes)
            {
                if (transientServiceInterfaceType.IsAssignableFrom(service.Interface))
                {
                    services.AddTransient(service.Interface, service.Implementation);
                }
                else if (singletonServiceInterfaceType.IsAssignableFrom(service.Implementation))
                {
                    services.AddSingleton(service.Interface, service.Implementation);
                }
                else if (scopedServiceInterfaceType.IsAssignableFrom(service.Implementation))
                {
                    services.AddScoped(service.Interface, service.Implementation);
                }
            }


            return services;
        }
    }
}
