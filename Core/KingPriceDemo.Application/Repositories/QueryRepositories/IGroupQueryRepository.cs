using KingPriceDemo.Application.Common.Repositories;

namespace KingPriceDemo.Application.Repositories.QueryRepositories
{
    public interface IGroupQueryRepository : ISecureQuery
    {
        IQueryable<Group> Groups { get; }

        IQueryable<Group> UnlockedGroups { get; }

        IQueryable<UserGroup> UserGroups { get; }
    }
}
