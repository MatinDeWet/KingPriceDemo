namespace KingPriceDemo.Persistence.Locks
{
    public class GroupInviteLock(KingPriceContext context) : Lock<GroupInvite>
    {
        public override async Task<bool> HasAccess(GroupInvite obj, int identityId, RepositoryOperationEnum operation, GroupRightsEnum requirement, CancellationToken cancellationToken)
        {
            if (operation == RepositoryOperationEnum.Update || operation == RepositoryOperationEnum.Delete)
                return false;

            var groupId = obj.GroupId;

            if (groupId == 0)
                return false;

            var query = from ug in context.Set<UserGroup>()
                        where ug.UserId == identityId
                            && ug.GroupId == groupId
                            && ug.Rights.HasFlag(requirement)
                        select ug;

            return await query.AnyAsync(cancellationToken);
        }

        public override IQueryable<GroupInvite> Secured(int identityId, GroupRightsEnum requirement)
        {
            var query = from gi in context.Set<GroupInvite>()
                        join ug in context.Set<UserGroup>() on gi.GroupId equals ug.GroupId
                        where ug.UserId == identityId
                            && ug.Rights.HasFlag(requirement)
                        select gi;

            return query;
        }
    }
}
