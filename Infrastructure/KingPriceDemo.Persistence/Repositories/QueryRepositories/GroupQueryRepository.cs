using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Persistence.Repositories.QueryRepositories
{
    public class GroupQueryRepository : JudgedQueries<KingPriceContext>, IGroupQueryRepository
    {
        public GroupQueryRepository(KingPriceContext context, IIdentityInfo info, AccessRequirements requirements, IEnumerable<IProtected> protection) : base(context, info, requirements, protection)
        {
        }

        public IQueryable<Group> Groups => Secure<Group>();
    }
}
