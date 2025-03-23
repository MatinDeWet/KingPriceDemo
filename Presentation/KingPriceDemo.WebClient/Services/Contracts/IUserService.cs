using KingPriceDemo.WebClient.Models;

namespace KingPriceDemo.WebClient.Services.Contracts
{
    public interface IUserService
    {
        Task<Response<UserModel>> GetLoggedInUser();

        Task<Response<int>> UpdateUser(UserModel model);

        Task<Response<int>> DeleteUser();
    }
}
