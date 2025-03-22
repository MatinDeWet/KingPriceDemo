using KingPriceDemo.WebClient.Models;

namespace KingPriceDemo.WebClient.Services.Contracts
{
    public interface IUserService
    {
        Task<UserModel> GetLoggedInUser();

        Task UpdateUser(UserModel model);

        Task DeleteUser();
    }
}
