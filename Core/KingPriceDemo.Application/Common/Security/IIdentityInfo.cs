namespace KingPriceDemo.Application.Common.Security
{
    public interface IIdentityInfo
    {
        int GetIdentityId();

        bool HasValue(string name);

        string GetValue(string name);
    }
}
