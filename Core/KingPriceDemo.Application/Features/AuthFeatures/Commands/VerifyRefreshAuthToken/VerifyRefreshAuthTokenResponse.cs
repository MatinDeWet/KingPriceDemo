namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.VerifyRefreshAuthToken
{
    public record VerifyRefreshAuthTokenResponse(int UserId, string Token, string RefreshToken);
}
