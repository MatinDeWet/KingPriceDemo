using KingPriceDemo.Application.Common.Pagination.Models;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.CreateGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.DeleteGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Commands.UpdateGroup;
using KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById;
using KingPriceDemo.Application.Features.GroupFeatures.Queries.SearchGroup;
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
    }
}
