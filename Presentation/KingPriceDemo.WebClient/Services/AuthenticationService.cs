using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Constants;
using KingPriceDemo.WebClient.Models;
using KingPriceDemo.WebClient.Providers;
using KingPriceDemo.WebClient.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace KingPriceDemo.WebClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly KingPriceHttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(KingPriceHttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<int> GetIdentityId()
        {
            var claims = await ((ApiAuthenticationStateProvider)authenticationStateProvider).GetClaims();

            if (claims != null && claims.Any())
            {
                var userIdString = claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier)
                    .Select(x => x.Value)
                    .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(userIdString))
                {
                    return int.Parse(userIdString);
                }
            }

            return 0;
        }

        public async Task<bool> AuthenticateAsync(LoginModel loginModel)
        {
            var dto = new AuthLoginRequest
            {
                Email = loginModel.Email,
                Password = loginModel.Password
            };

            var response = await httpClient.ApiAuthLoginAsync(dto);

            await localStorage.SetItemAsync(LocalStorageConstants.Token, response.Token);
            await localStorage.SetItemAsync(LocalStorageConstants.UserID, response.UserId);
            await localStorage.SetItemAsync(LocalStorageConstants.RefreshToken, response.RefreshToken);


            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
