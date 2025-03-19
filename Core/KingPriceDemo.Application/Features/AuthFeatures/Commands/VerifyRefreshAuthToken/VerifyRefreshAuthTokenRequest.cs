namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.VerifyRefreshAuthToken
{
    public record VerifyRefreshAuthTokenRequest(int UserId, string Token, string RefreshToken) : ICommand<VerifyRefreshAuthTokenResponse>;
}
