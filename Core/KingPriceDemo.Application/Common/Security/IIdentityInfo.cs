using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Application.Common.Security
{
    public interface IIdentityInfo
    {
        int GetIdentityId();

        bool HasRole(ApplicationRoleEnum role);

        bool HasValue(string name);

        string GetValue(string name);
    }
}
