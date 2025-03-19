namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthLogin
{
    public record AuthLoginRequest(string Email, string Password) : ICommand<AuthLoginResponse>;
}
