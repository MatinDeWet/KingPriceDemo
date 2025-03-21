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
