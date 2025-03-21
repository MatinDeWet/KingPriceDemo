using KingPriceDemo.Application.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KingPriceDemo.Persistence
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection AddDataSecurity(this IServiceCollection services)
        {
            services.AddScoped<AccessRequirements>();

            return services;
        }

        public static IServiceCollection AddRepos(this IServiceCollection services, Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses((classes) => classes.AssignableToAny(typeof(ISecureQuery), typeof(ISecureCommand)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddRepoLocks(this IServiceCollection services, Assembly assembly)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses((classes) => classes.AssignableToAny(typeof(IProtected)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
