using System.Security.Claims;

namespace KingPriceDemo.Application.Common.Security
{
    public interface IInfoSetter : IList<Claim>
    {
        void SetUser(IEnumerable<Claim> claims);
    }
}
