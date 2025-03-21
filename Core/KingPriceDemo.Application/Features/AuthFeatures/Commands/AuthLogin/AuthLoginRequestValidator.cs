namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthLogin
{
    public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequest>
    {
        public AuthLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .StringInput(256)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.Password)
                .StringInput(512);
        }
    }
}
