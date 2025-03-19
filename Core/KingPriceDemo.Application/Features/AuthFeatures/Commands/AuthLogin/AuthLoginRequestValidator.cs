namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthLogin
{
    public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequest>
    {
        public AuthLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .SetValidator(new StringInputValidator())
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.Password)
                .SetValidator(new StringInputValidator());
        }
    }
}
