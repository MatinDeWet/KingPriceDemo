using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthRegister
{
    public class AuthRegisterRequest : ICommand<IEnumerable<IdentityError>>
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
