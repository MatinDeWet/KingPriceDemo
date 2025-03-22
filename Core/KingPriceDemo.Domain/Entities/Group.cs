using KingPriceDemo.Domain.Common.Abstractions;

namespace KingPriceDemo.Domain.Entities
{
    public class Group : Entity<int>
    {
        public string Name { get; set; } = null!;

        public string InviteToken { get; set; } = null!;

        public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
