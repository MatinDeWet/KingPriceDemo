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
            var query = from u in context.Set<User>()
                        let groupIds = from ug in context.Set<UserGroup>()
                                       where ug.UserId == identityId
                                         && ug.Rights.HasFlag(requirement)
                                       select ug.GroupId
                        where u.Id == identityId ||
                              u.UserGroups.Any(ug => groupIds.Contains(ug.GroupId))
                        select u;

            return query;
        }
    }
}
