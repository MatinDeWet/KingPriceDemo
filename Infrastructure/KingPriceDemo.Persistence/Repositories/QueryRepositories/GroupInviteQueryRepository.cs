using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Persistence.Repositories.QueryRepositories
{
    public class GroupInviteQueryRepository : JudgedQueries<KingPriceContext>, IGroupInviteQueryRepository
    {
        public GroupInviteQueryRepository(KingPriceContext context, IIdentityInfo info, AccessRequirements requirements, IEnumerable<IProtected> protection) : base(context, info, requirements, protection)
        {
        }

        public IQueryable<GroupInvite> GroupInvites => Secure<GroupInvite>();
    }
}
