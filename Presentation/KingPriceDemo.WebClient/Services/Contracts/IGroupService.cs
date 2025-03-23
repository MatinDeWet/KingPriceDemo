using KingPriceDemo.WebClient.Models;

namespace KingPriceDemo.WebClient.Services.Contracts
{
    public interface IGroupService
    {
        Task<Response<List<GroupListModel>>> GetGroupList();

        Task<Response<GroupDetailModel>> GetGroupById(int id);

        Task<Response<int>> CreateGroup(string name);

        Task<Response<int>> UpdateGroup(int id, string name);

        Task<Response<int>> DeleteGroup(int id);

        Task<Response<int>> RefreshGroupInviteToken(int id);

        Task<Response<int>> JoinGroupWithToken(string token);

        Task<Response<int>> LeaveGroup(int id);
    }
}
