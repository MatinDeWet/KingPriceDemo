namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.JoinGroupWithToken
{
    public record JoinGroupWithTokenRequest(string Token) : ICommand;
}
