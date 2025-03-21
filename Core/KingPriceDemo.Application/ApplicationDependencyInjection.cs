using KingPriceDemo.Application.Common.Behaviors;
using KingPriceDemo.Application.Common.Exceptions.Handler;
using KingPriceDemo.Application.Common.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KingPriceDemo.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddMediatRBehavior(this IServiceCollection services, Assembly assembly)
        {
            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }

        public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = (context) =>
                {
                    context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                    var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);

                };
            });

            return services;
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options => { });

            return app;
        }

        public static IServiceCollection AddDataIdentity(this IServiceCollection services)
        {
            services.AddScoped<IIdentityInfo, IdentityInfo>();
            services.AddScoped<IInfoSetter, InfoSetter>();

            return services;
        }
    }
}
