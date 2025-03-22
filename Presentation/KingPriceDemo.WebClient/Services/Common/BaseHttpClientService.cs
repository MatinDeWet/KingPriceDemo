using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Constants;
using System.Net.Http.Headers;

namespace KingPriceDemo.WebClient.Services.Common
{
    public abstract class BaseHttpClientService
    {
        protected readonly KingPriceHttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        protected BaseHttpClientService(KingPriceHttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        protected async Task SetToken()
        {
            var token = await _localStorage.GetItemAsync<string>(LocalStorageConstants.Token);

            if (string.IsNullOrWhiteSpace(token))
            {
                return;
            }

            _httpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
