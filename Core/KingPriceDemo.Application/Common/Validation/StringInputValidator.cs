namespace KingPriceDemo.Application.Common.Validation
{
    public class StringInputValidator : AbstractValidator<string?>
    {
        public StringInputValidator(int maxLength, bool isRequired = true)
        {
            if (isRequired)
            {
                RuleFor(x => x)
                    .NotEmpty()
                    .WithMessage("{PropertyName} is required.");
            }

            RuleFor(x => x)
                .MaximumLength(maxLength)
                .WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
        }

        public StringInputValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
