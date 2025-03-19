using KingPriceDemo.Application.Features.AuthFeatures.Commands.GenerateAuthToken;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.RefreshAuthToken;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthLogin
{
    public class AuthLoginHandler(UserManager<ApplicationUser> userManager, ISender sender)
            : ICommandHandler<AuthLoginRequest, AuthLoginResponse>
    {
        private ApplicationUser _user = null!;

        public async Task<AuthLoginResponse> Handle(AuthLoginRequest request, CancellationToken cancellationToken)
        {
            _user = await userManager.FindByEmailAsync(request.Email) ?? null!;

            if (_user is null)
                return null!;

            var validPassword = await userManager.CheckPasswordAsync(_user, request.Password);

            if (!validPassword)
                return null!;

            var token = await sender.Send(new GenerateAuthTokenRequest(_user));
            var refreshToken = await sender.Send(new RefreshAuthTokenRequest(_user));

            return new AuthLoginResponse(_user.Id, token, refreshToken);
        }
    }
}
