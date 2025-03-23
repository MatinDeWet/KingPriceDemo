using KingPriceDemo.WebClient.Models;

namespace KingPriceDemo.WebClient.Services.Contracts
{
    public interface IGroupService
    {
        Task<List<GroupListModel>> GetGroupList();

        Task<GroupDetailModel> GetGroupById(int id);

        Task<int> CreateGroup(string name);

        Task UpdateGroup(int id, string name);

        Task DeleteGroup(int id);

        Task RefreshGroupInviteToken(int id);

        Task JoinGroupWithToken(string token);

        Task LeaveGroup(int id);
    }
}
