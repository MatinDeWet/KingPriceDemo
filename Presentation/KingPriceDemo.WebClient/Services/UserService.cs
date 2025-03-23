using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Models;
using KingPriceDemo.WebClient.Services.Base;
using KingPriceDemo.WebClient.Services.Contracts;
using System.Reflection;

namespace KingPriceDemo.WebClient.Services
{
    public class UserService : BaseHttpClientService, IUserService
    {
        private readonly IAuthenticationService _authenticationService;

        public UserService(KingPriceHttpClient httpClient, ILocalStorageService localStorage, IAuthenticationService authenticationService) : base(httpClient, localStorage)
        {
            _authenticationService = authenticationService;
        }

        public async Task<UserModel> GetLoggedInUser()
        {
            var userId = await _authenticationService.GetIdentityId();

            if (userId == 0)
                throw new UnauthorizedAccessException();

            await SetToken();

            var user = await _httpClient.ApiUserGetUserByIdAsync(userId);

            var userModel = new UserModel
            {
                FullName = user.FullName,
                Surname = user.Surname,
                Email = user.Email,
                CellphoneNumber = user.CellphoneNumber
            };

            return userModel;
        }

        public async Task UpdateUser(UserModel model)
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

            await SetToken();

            await _httpClient.ApiUserUpdateUserAsync(user);
        }

        public async Task DeleteUser()
        {
            await SetToken();
            await _httpClient.ApiUserDeleteUserAsync();
        }
    }
}
