using KingPriceDemo.Application.Common.Constants;
using KingPriceDemo.Domain.Entities;
using KingPriceDemo.Persistence.Data.Context;
using KingPriceDemo.WebApi.Common.Transformers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Text;

namespace KingPriceDemo.WebApi
{
    public static class WebApiDependencyInjection
    {
        public static IServiceCollection AddPreparedOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });

            return services;
        }

        public static IEndpointRouteBuilder UsePreparedOpenApi(this IEndpointRouteBuilder builder, bool isLocked)
        {
            if (!isLocked)
            {
                builder.MapOpenApi();
            }

            return builder;
        }

        public static IEndpointRouteBuilder UseScalarApiDocumentation(this IEndpointRouteBuilder builder, bool isLocked, IConfiguration configuration)
        {
            if (!isLocked)
            {
                var servers = configuration.GetSection("ScalarExplorer:Servers").Get<List<ScalarServer>>();
                var title = configuration.GetValue<string>("ScalarExplorer:Title");


                builder.MapScalarApiReference(options =>
                {
                    options.WithTitle(title!)
                    .WithTheme(ScalarTheme.Moon)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                    .WithDarkMode(true)
                    .WithPreferredScheme("Bearer")
                    .WithHttpBearerAuthentication(bearer =>
                    {
                        bearer.Token = "your-bearer-token";
                    });

                    options.Servers = servers;
                });
            }

            return builder;
        }

        public static IHostBuilder AddSerilog(this IHostBuilder webBuilder)
        {
            webBuilder.UseSerilog((ctx, lc) =>
            {
                lc.ReadFrom.Configuration(ctx.Configuration);
            });

            return webBuilder;
        }

        public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }

        public static IServiceCollection AddPreparedControllers(this IServiceCollection services, bool suppressModelStateInvalidFilter = true)
        {
            services.AddControllers();

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = suppressModelStateInvalidFilter;
            });

            return services;
        }

        public static IApplicationBuilder UseAuthenticationAndAuthorization(this IApplicationBuilder builder)
        {
            builder.UseAuthentication()
                .UseAuthorization();

            return builder;
        }

        public static IEndpointRouteBuilder UsePreparedControllers(this IEndpointRouteBuilder builder)
        {
            builder.MapControllers()
                .RequireAuthorization();

            return builder;
        }

        public static IServiceCollection AddPreparedAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            return services;
        }

        public static IServiceCollection AddIdentityPrepration(this IServiceCollection services)
        {
            services
                .AddIdentityCore<ApplicationUser>(options => { })
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(AuthConstants.LoginProvider)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<KingPriceContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
