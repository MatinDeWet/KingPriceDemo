namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.GenerateAuthToken
{
    public record GenerateAuthTokenRequest(ApplicationUser user) : ICommand<string>;
}
