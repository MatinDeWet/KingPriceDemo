using KingPriceDemo.WebClient.Models;

namespace KingPriceDemo.WebClient.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<int> GetIdentityId();

        Task<bool> AuthenticateAsync(LoginModel loginModel);

        public Task Logout();
    }
}
