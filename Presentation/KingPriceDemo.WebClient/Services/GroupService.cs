using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Models;
using KingPriceDemo.WebClient.Services.Common;
using KingPriceDemo.WebClient.Services.Contracts;

namespace KingPriceDemo.WebClient.Services
{
    public class GroupService : BaseHttpClientService, IGroupService
    {
        public GroupService(KingPriceHttpClient httpClient, ILocalStorageService localStorage) : base(httpClient, localStorage)
        {
        }

        public async Task<List<GroupListModel>> GetGroupList()
        {
            var request = new SearchGroupRequest
            {
                OrderDirection = 1,
                PageNumber = 1,
                PageSize = 100
            };

            await SetToken();
            var response = await _httpClient.ApiGroupSearchGroupAsync(request);

            return response.Data
                .Select(x => new GroupListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserCount = x.UserCount
                })
                .ToList();
        }

        public async Task<GroupDetailModel> GetGroupById(int id)
        {
            await SetToken();
            var response = await _httpClient.ApiGroupGetGroupByIdAsync(id);

            return new GroupDetailModel
            {
                Id = response.Id,
                Name = response.Name,
                InviteToken = response.InviteToken,
                Users = response.Users
                    .Select(x => new GroupDetailUserListModel
                    {
                        Id = x.Id,
                        Email = x.Email,
                        Rights = x.Rights,
                        RightsText = x.RightsText,
                        FullName = x.FullName,
                        Surname = x.Surname
                    })
                    .ToList()
            };
        }

        public async Task<int> CreateGroup(string name)
        {
            var request = new CreateGroupRequest
            {
                Name = name
            };

            await SetToken();

            var response = await _httpClient.ApiGroupCreateGroupAsync(request);

            return response.Id;
        }

        public async Task UpdateGroup(int id, string name)
        {
            var request = new UpdateGroupRequest
            {
                Id = id,
                Name = name
            };

            await SetToken();

            await _httpClient.ApiGroupUpdateGroupAsync(request);
        }

        public async Task DeleteGroup(int id)
        {
            await SetToken();
            await _httpClient.ApiGroupDeleteGroupAsync(id);
        }

        public async Task RefreshGroupInviteToken(int id)
        {
            await SetToken();
            await _httpClient.ApiGroupRefreshGroupInviteTokenAsync(id);
        }

        public async Task JoinGroupWithToken(string token)
        {
            var request = new JoinGroupWithTokenRequest
            {
                Token = token
            };
            await SetToken();
            await _httpClient.ApiGroupJoinGroupWithTokenAsync(request);
        }

        public async Task LeaveGroup(int id)
        {
            await SetToken();
            await _httpClient.ApiGroupLeaveGroupAsync(id);
        }
    }
}
