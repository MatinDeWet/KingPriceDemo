using KingPriceDemo.Domain.Enums;
using System.Security.Claims;

namespace KingPriceDemo.Application.Common.Security
{
    public class IdentityInfo : IIdentityInfo
    {
        private readonly IInfoSetter _infoSetter;

        public IdentityInfo(IInfoSetter infoSetter)
        {
            _infoSetter = infoSetter;
        }

        public bool HasRole(ApplicationRoleEnum role)
        {
            var roles = _infoSetter
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList();

            ApplicationRoleEnum combinedRoles = ApplicationRoleEnum.None;

            foreach (var roleString in roles)
                if (Enum.TryParse(roleString, true, out ApplicationRoleEnum parsedRole))
                    combinedRoles |= parsedRole;

            return combinedRoles.HasFlag(role);
        }

        public int GetIdentityId()
        {
            var a = _infoSetter.ToList();

            var uid = GetValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(uid, out int result))
                return 0;

            return result;
        }

        public string GetValue(string name)
        {
            var claim = _infoSetter.FirstOrDefault(x => x.Type == name);
            return claim == null ? null! : claim.Value;
        }

        public bool HasValue(string name)
        {
            return _infoSetter.Any(x => x.Type == name);
        }
    }
}
