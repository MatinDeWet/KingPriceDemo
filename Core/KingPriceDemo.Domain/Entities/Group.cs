using KingPriceDemo.Domain.Common.Abstractions;

namespace KingPriceDemo.Domain.Entities
{
    public class Group : Entity<int>
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public virtual ICollection<GroupInvite> Invites { get; set; } = new List<GroupInvite>();
    }
}
