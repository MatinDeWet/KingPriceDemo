using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Models;
using KingPriceDemo.WebClient.Services.Base;
using KingPriceDemo.WebClient.Services.Contracts;

namespace KingPriceDemo.WebClient.Services
{
    public class UserService : BaseHttpClientService, IUserService
    {
        private readonly IAuthenticationService _authenticationService;

        public UserService(KingPriceHttpClient httpClient, ILocalStorageService localStorage, IAuthenticationService authenticationService) : base(httpClient, localStorage)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Response<UserModel>> GetLoggedInUser()
        {
            var userId = await _authenticationService.GetIdentityId();

            if (userId == 0)
                throw new UnauthorizedAccessException();

            var response = new Response<UserModel>();

            try
            {
                await SetToken();
                var apiResponse = await _httpClient.ApiUserGetUserByIdAsync(userId);

                response.Data = new UserModel
                {
                    FullName = apiResponse.FullName,
                    Surname = apiResponse.Surname,
                    Email = apiResponse.Email,
                    CellphoneNumber = apiResponse.CellphoneNumber
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<UserModel>(exception);
            }

            return response;
        }

        public async Task<Response<int>> UpdateUser(UserModel model)
        {
            var userId = await _authenticationService.GetIdentityId();

            if (userId == 0)
                throw new UnauthorizedAccessException();

            var user = new UpdateUserRequest
            {
                FullName = model.FullName,
                Surname = model.Surname,
                Email = model.Email,
                CellphoneNumber = model.CellphoneNumber
            };

            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiUserUpdateUserAsync(user);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> DeleteUser()
        {
            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiUserDeleteUserAsync();
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }
    }
}
