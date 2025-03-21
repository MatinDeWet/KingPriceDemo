using KingPriceDemo.Application;
using KingPriceDemo.Persistence;
using KingPriceDemo.Persistence.Data.Context;
using KingPriceDemo.WebApi;
using KingPriceDemo.WebApi.Common.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.AddSerilog();
        builder.Services.AddCustomExceptionHandler();

        builder.Services
            .AddPreparedControllers()
            .AddPreparedOpenApi()
            .AddPreparedAuthentication(builder.Configuration)
            .AddDataIdentity()
            .AddIdentityPrepration();

        builder.Services.AddMediatRBehavior(typeof(ApplicationDependencyInjection).Assembly);

        builder.Services
            .AddDbContext<KingPriceContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly(typeof(KingPriceContext).GetTypeInfo().Assembly.GetName().Name));

                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            })
            .AddDataSecurity()
            .AddRepos(typeof(PersistenceDependencyInjection).Assembly)
            .AddRepoLocks(typeof(PersistenceDependencyInjection).Assembly);

        var app = builder.Build();

        app.UsePreparedOpenApi(!builder.Environment.IsDevelopment());

        app.UseScalarApiDocumentation(
            !builder.Environment.IsDevelopment(),
            builder.Configuration
        );

        app.UseSerilog();
        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthenticationAndAuthorization();

        app.UsePreparedControllers();

        app.UseMiddleware<UserIdentifierMiddleware>();

        if (builder.Environment.IsDevelopment())
        {
            ApplyDbMigrations(app);
        }

        app.Run();
    }

    internal static void ApplyDbMigrations(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

        if (serviceScope.ServiceProvider.GetRequiredService<KingPriceContext>().Database.GetPendingMigrations().Count() > 0)
            serviceScope.ServiceProvider.GetRequiredService<KingPriceContext>().Database.Migrate();
    }
}
