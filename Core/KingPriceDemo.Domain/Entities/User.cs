using KingPriceDemo.Domain.Common.Abstractions;

namespace KingPriceDemo.Domain.Entities
{
    public class User : Entity<int>
    {
        public virtual ApplicationUser IdentityInfo { get; set; } = null!;

        public string? FullName { get; set; }

        public string? Surname { get; set; }

        public string Email { get; set; } = null!;

        public string? CellphoneNumber { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public virtual ICollection<GroupInvite> AcceptedInvites { get; set; } = new List<GroupInvite>();
    }
}
