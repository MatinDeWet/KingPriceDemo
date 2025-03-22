namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.UpdateGroup
{
    public record UpdateGroupRequest(int Id, string Name) : ICommand;
}
