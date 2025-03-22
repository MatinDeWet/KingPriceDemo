namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.CreateGroup
{
    public record CreateGroupRequest(string Name) : ICommand<CreateGroupResponse>;
}
