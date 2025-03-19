using KingPriceDemo.Application;
using KingPriceDemo.WebApi;

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
            .AddPreparedAuthentication(builder.Configuration);

        builder.Services.AddMediatRBehavior(typeof(ApplicationDependencyInjection).Assembly);

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

        app.Run();
    }
}
