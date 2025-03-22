using KingPriceDemo.Application.Common.Extensions;
using KingPriceDemo.Application.Common.Models;
using KingPriceDemo.Application.Common.Pagination.Models;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.CreateGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.DeleteGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.JoinGroupWithToken;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.LeaveGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.RefreshGroupInviteToken;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.UpdateGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById;
using KingPriceDemo.Application.Features.GroupFeatures.Queries.SearchGroup;
using KingPriceDemo.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KingPriceDemo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GroupController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetGroupByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupById([FromQuery] int id)
        {
            var response = await sender.Send(new GetGroupByIdRequest(id));
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasicList), StatusCodes.Status200OK)]
        public IActionResult GetGroupRights()
        {
            var list = EnumTools.GetValuesAndDisplayNames<GroupRightsEnum>();
            return Ok(list);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PageableResponse<SearchGroupResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchGroup([FromBody] SearchGroupRequest request)
        {
            var response = await sender.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateGroupResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var response = await sender.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest request)
        {
            await sender.Send(request);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGroup([FromQuery] int id)
        {
            await sender.Send(new DeleteGroupRequest(id));
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> JoinGroupWithToken([FromBody] JoinGroupWithTokenRequest request)
        {
            await sender.Send(request);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> LeaveGroup([FromQuery] int id)
        {
            await sender.Send(new LeaveGroupRequest(id));
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RefreshGroupInviteToken([FromQuery] int id)
        {
            await sender.Send(new RefreshGroupInviteTokenRequest(id));
            return NoContent();
        }
    }
}
