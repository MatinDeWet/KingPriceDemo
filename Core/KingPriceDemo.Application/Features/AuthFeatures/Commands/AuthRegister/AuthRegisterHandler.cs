using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthRegister
{
    public class AuthRegisterHandler(UserManager<ApplicationUser> userManager)
            : ICommandHandler<AuthRegisterRequest, IEnumerable<IdentityError>>
    {
        private ApplicationUser _user = null!;

        public async Task<IEnumerable<IdentityError>> Handle(AuthRegisterRequest request, CancellationToken cancellationToken)
        {
            _user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await userManager.CreateAsync(_user, request.Password);

            return result.Errors;
        }
    }
}
