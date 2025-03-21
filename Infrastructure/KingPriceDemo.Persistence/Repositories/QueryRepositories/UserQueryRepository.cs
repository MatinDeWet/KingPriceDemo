using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.QueryRepositories;

namespace KingPriceDemo.Persistence.Repositories.QueryRepositories
{
    public class UserQueryRepository : JudgedQueries<KingPriceContext>, IUserQueryRepository
    {
        public UserQueryRepository(KingPriceContext context, IIdentityInfo info, AccessRequirements requirements, IEnumerable<IProtected> protection) : base(context, info, requirements, protection)
        {
        }

        public IQueryable<User> Users => Secure<User>();
    }
}
