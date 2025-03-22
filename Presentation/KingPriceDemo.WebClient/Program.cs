using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Components;
using KingPriceDemo.WebClient.Providers;
using KingPriceDemo.WebClient.Services;
using KingPriceDemo.WebClient.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication("Cookies").AddCookie();

builder.Services.AddHttpClient<KingPriceHttpClient>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ApiResources:KingPriceApi:BaseUrl"]!);
}).AddHttpMessageHandler(sp =>
{
    return new TokenValidationService(sp.GetRequiredService<IHttpContextAccessor>());
});

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(options =>
    options.GetRequiredService<ApiAuthenticationStateProvider>()
);

builder.Services.AddTransient<TokenValidationService>();


builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
