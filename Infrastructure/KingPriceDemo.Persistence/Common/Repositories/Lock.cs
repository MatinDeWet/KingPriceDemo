using KingPriceDemo.Domain.Enums;
using KingPriceDemo.Persistence.Common.Repositories.Enums;

namespace KingPriceDemo.Persistence.Common.Repositories
{
    public abstract class Lock<T> : IProtected<T> where T : class
    {
        public abstract IQueryable<T> Secured(int identityId, GroupRightsEnum requirement);

        public abstract Task<bool> HasAccess(T obj, int identityId, RepositoryOperationEnum operation, GroupRightsEnum requirement, CancellationToken cancellationToken);

        public virtual bool IsMatch(Type t)
        {
            return typeof(T).IsAssignableFrom(t);
        }
    }
}
