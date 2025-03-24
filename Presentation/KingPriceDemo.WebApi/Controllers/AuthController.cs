using KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthLogin;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.AuthRegister;
using KingPriceDemo.Application.Features.AuthFeatures.Commands.VerifyRefreshAuthToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingPriceDemo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);

            if (result.Any())
            {
                foreach (var error in result)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);

            if (result is null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(VerifyRefreshAuthTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] VerifyRefreshAuthTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);

            if (result is null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
