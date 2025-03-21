namespace KingPriceDemo.Persistence.Locks
{
    public class GroupLock(KingPriceContext context) : Lock<Group>
    {
        public override async Task<bool> HasAccess(Group obj, int identityId, RepositoryOperationEnum operation, GroupRightsEnum requirement, CancellationToken cancellationToken)
        {
            if (operation == RepositoryOperationEnum.Insert)
            {
                var userId = obj.UserGroups.Select(ug => ug.UserId).FirstOrDefault();

                if (userId == 0)
                    return false;

                return userId == identityId;
            }

            var groupId = obj.Id;

            if (groupId == 0)
                return false;


            var query = from ug in context.Set<UserGroup>()
                        where ug.UserId == identityId
                            && ug.GroupId == groupId
                            && ug.Rights.HasFlag(GroupRightsEnum.Owner)
                        select ug;

            return await query.AnyAsync(cancellationToken);
        }

        public override IQueryable<Group> Secured(int identityId, GroupRightsEnum requirement)
        {
            var query = from g in context.Set<Group>()
                        join ug in context.Set<UserGroup>() on g.Id equals ug.GroupId
                        where ug.UserId == identityId
                            && ug.Rights.HasFlag(requirement)
                        select g;

            return query;
        }
    }
}
