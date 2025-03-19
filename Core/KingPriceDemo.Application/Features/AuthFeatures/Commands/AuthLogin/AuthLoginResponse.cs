namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthLogin
{
    public record AuthLoginResponse(int UserId, string Token, string RefreshToken);
}
