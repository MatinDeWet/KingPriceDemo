using KingPriceDemo.Application.Common.Pagination.Models;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthDeleteUser;
using KingPriceDemo.Application.Features.UserFeatures.Commands.UpdateUser;
using KingPriceDemo.Application.Features.UserFeatures.Queries.GetUserById;
using KingPriceDemo.Application.Features.UserFeatures.Queries.SearchUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KingPriceDemo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var response = await sender.Send(new GetUserByIdRequest(id));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PageableResponse<SearchUserResponse>), StatusCodes.Status200OK)]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchUser([FromBody] SearchUserRequest request)
        {
            var response = await sender.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser()
        {
            await sender.Send(new AuthDeleteUserRequest());

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            await sender.Send(request);

            return Ok();
        }
    }
}
