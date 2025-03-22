
using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace KingPriceDemo.WebClient.Services
{
    public class TokenValidationService : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public TokenValidationService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var savedToken = await _localStorage.GetItemAsync<string>(LocalStorageConstants.Token, cancellationToken);

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                var response = await base.SendAsync(request, cancellationToken);
                return response;
            }

            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

            if (tokenContent.ValidTo > DateTime.Now)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
