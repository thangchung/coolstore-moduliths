using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Moduliths.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceByIntefaceInAssembly<TRegisteredAssemblyType>(this IServiceCollection services, Type interfaceType)
        {
            services.Scan(s =>
                s.FromAssemblyOf<TRegisteredAssemblyType>()
                    .AddClasses(c => c.AssignableTo(interfaceType))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddServiceInAssembly(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies == null) return services;

            services.Scan(s => s
                .FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IDependency)))
                .AsSelf()
                .WithScopedLifetime());

            return services;
        }
    }

    public interface IDependency { }
}
