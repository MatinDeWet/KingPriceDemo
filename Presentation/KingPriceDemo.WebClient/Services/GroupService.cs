using Blazored.LocalStorage;
using KingPriceDemo.ApiClient;
using KingPriceDemo.WebClient.Models;
using KingPriceDemo.WebClient.Services.Base;
using KingPriceDemo.WebClient.Services.Contracts;

namespace KingPriceDemo.WebClient.Services
{
    public class GroupService : BaseHttpClientService, IGroupService
    {
        public GroupService(KingPriceHttpClient httpClient, ILocalStorageService localStorage) : base(httpClient, localStorage)
        {
        }

        public async Task<Response<List<GroupListModel>>> GetGroupList()
        {
            var request = new SearchGroupRequest
            {
                OrderDirection = 1,
                PageNumber = 1,
                PageSize = 100
            };


            var response = new Response<List<GroupListModel>>();

            try
            {
                await SetToken();
                var apiResponse = await _httpClient.ApiGroupSearchGroupAsync(request);

                response.Data = apiResponse.Data
                    .Select(x => new GroupListModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UserCount = x.UserCount,
                        GroupRights = x.GroupRights,
                        GroupRightsText = x.GroupRightsText
                    })
                    .ToList();
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<List<GroupListModel>>(exception);
            }

            return response;
        }

        public async Task<Response<GroupDetailModel>> GetGroupById(int id)
        {
            var response = new Response<GroupDetailModel>();

            try
            {
                await SetToken();
                var apiReponse = await _httpClient.ApiGroupGetGroupByIdAsync(id);

                response.Data = new GroupDetailModel
                {
                    Id = apiReponse.Id,
                    Name = apiReponse.Name,
                    InviteToken = apiReponse.InviteToken,
                    GroupRights = apiReponse.GroupRights,
                    GroupRightsText = apiReponse.GroupRightsText,
                    Users = apiReponse.Users
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
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<GroupDetailModel>(exception);
            }

            return response;
        }

        public async Task<Response<int>> CreateGroup(string name)
        {
            var request = new CreateGroupRequest
            {
                Name = name
            };

            var response = new Response<int>();

            try
            {
                await SetToken();

                var apiReponse = await _httpClient.ApiGroupCreateGroupAsync(request);

                response.Data = apiReponse.Id;
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }



            return response;
        }

        public async Task<Response<int>> UpdateGroup(int id, string name)
        {
            var request = new UpdateGroupRequest
            {
                Id = id,
                Name = name
            };

            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiGroupUpdateGroupAsync(request);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> DeleteGroup(int id)
        {
            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiGroupDeleteGroupAsync(id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> RefreshGroupInviteToken(int id)
        {
            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiGroupRefreshGroupInviteTokenAsync(id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> JoinGroupWithToken(string token)
        {
            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiGroupJoinGroupWithTokenAsync(new JoinGroupWithTokenRequest { Token = token });
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> LeaveGroup(int id)
        {
            var response = new Response<int>();

            try
            {
                await SetToken();
                await _httpClient.ApiGroupLeaveGroupAsync(id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }
    }
}
