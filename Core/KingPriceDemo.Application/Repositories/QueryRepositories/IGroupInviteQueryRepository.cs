using KingPriceDemo.Application.Common.Repositories;

namespace KingPriceDemo.Application.Repositories.QueryRepositories
{
    public interface IGroupInviteQueryRepository : ISecureQuery
    {
        IQueryable<GroupInvite> GroupInvites { get; }
    }
}
