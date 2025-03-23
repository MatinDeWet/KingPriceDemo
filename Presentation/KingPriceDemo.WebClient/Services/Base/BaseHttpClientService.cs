using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Constants;
using KingPriceDemo.WebClient.Models;
using System.Net.Http.Headers;

namespace KingPriceDemo.WebClient.Services.Base
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
                return;

            _httpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
                return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };

            if (apiException.StatusCode == 404)
                return new Response<Guid>() { Message = "The requested item could not be found.", Success = false };

            if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299)
                return new Response<Guid>() { Message = "Operation Reported Success", Success = true };

            return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
        }
    }
}
