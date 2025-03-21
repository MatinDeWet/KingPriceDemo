using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KingPriceDemo.Application.Features.AuthFeatures.Commands.GenerateAuthToken
{
    public class GenerateAuthTokenHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
            : ICommandHandler<GenerateAuthTokenRequest, string>
    {
        public async Task<string> Handle(GenerateAuthTokenRequest request, CancellationToken cancellationToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(request.user);

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await userManager.GetClaimsAsync(request.user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(roleClaims)
            .Union(userClaims);

            var tokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: tokenExpiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
