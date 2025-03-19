namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.RefreshAuthToken
{
    public record RefreshAuthTokenRequest(ApplicationUser user) : ICommand<string>;
}
