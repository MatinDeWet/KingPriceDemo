using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Domain.Entities
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int GroupId { get; set; }
        public virtual Group Group { get; set; } = null!;

        public GroupRightsEnum Rights { get; set; }
    }
}
