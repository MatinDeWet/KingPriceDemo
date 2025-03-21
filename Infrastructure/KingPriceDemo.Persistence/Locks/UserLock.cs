namespace KingPriceDemo.Persistence.Locks
{
    public class UserLock(KingPriceContext context) : Lock<User>
    {
        public override async Task<bool> HasAccess(User obj, int identityId, RepositoryOperationEnum operation, GroupRightsEnum requirement, CancellationToken cancellationToken)
        {
            if (operation == RepositoryOperationEnum.Insert)
                return await Task.FromResult(true);

            var userId = obj.Id;

            if (userId == 0)
                return false;

            return userId == identityId;
        }

        public override IQueryable<User> Secured(int identityId, GroupRightsEnum requirement)
        {
            var allowedGroupIds = from ug in context.Set<UserGroup>()
                                  where ug.UserId == identityId
                                     && ug.Rights.HasFlag(requirement)
                                  select ug.GroupId;

            var selfQuery = from u in context.Set<User>()
                            where u.Id == identityId
                            select u;

            var groupUsersQuery = from u in context.Set<User>()
                                  join ug in context.Set<UserGroup>() on u.Id equals ug.UserId
                                  where allowedGroupIds.Contains(ug.GroupId)
                                  select u;

            var securedUsersQuery = selfQuery.Union(groupUsersQuery);

            return securedUsersQuery;
        }
    }
}
