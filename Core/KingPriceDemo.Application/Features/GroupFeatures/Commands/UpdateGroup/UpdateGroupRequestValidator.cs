namespace KingPriceDemo.Application.Features.GroupFeatures.Commands.UpdateGroup
{
    public class UpdateGroupRequestValidator : AbstractValidator<UpdateGroupRequest>
    {
        public UpdateGroupRequestValidator()
        {
            RuleFor(x => x.Name)
                .StringInput(64);
        }
    }
}
