using KingPriceDemo.Application.Common.Security;
using KingPriceDemo.Application.Repositories.CommandRepositories;

namespace KingPriceDemo.Persistence.Repositories.CommandRepositories
{
    public class GroupCommandRepository : JudgedCommands<KingPriceContext>, IGroupCommandRepository
    {
        public GroupCommandRepository(KingPriceContext context, IIdentityInfo info, AccessRequirements requirements, IEnumerable<IProtected> protection) : base(context, info, requirements, protection)
        {
        }
    }
}
