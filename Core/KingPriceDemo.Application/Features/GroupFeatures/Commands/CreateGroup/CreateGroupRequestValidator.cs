namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.CreateGroup
{
    public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
    {
        public CreateGroupRequestValidator()
        {
            RuleFor(x => x.Name)
                .StringInput(64);
        }
    }
}
