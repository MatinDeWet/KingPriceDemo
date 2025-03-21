using KingPriceDemo.Application.Common.Repositories;

namespace KingPriceDemo.Application.Repositories.QueryRepositories
{
    public interface IUserQueryRepository : ISecureQuery
    {
        IQueryable<User> Users { get; }
    }
}
