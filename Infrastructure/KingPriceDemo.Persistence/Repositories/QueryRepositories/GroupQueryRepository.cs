using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Persistence.Repositories.QueryRepositories
{
    public class GroupQueryRepository : JudgedQueries<KingPriceContext>, IGroupQueryRepository
    {
        private readonly KingPriceContext _context;

        public GroupQueryRepository(KingPriceContext context, IIdentityInfo info, AccessRequirements requirements, IEnumerable<IProtected> protection) : base(context, info, requirements, protection)
        {
            _context = context;
        }

        public IQueryable<Group> Groups => Secure<Group>();

        public IQueryable<Group> UnlockedGroups => _context.Set<Group>();

        public IQueryable<UserGroup> UserGroups => Secure<UserGroup>();
    }
}
