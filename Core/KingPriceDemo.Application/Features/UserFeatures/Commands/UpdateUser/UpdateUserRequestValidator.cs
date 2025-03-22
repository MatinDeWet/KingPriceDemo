namespace KingPriceDemo.Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.FullName)
                .StringInput(128, false);

            RuleFor(x => x.Surname)
                .StringInput(64, false);

            RuleFor(x => x.Email)
                .StringInput(256)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.CellphoneNumber)
                .StringInput(16, false);

        }
    }
}
