namespace KingPriceDemo.Domain.Entities
{
    public class GroupInvite
    {
        public Guid Id { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; } = null!;

        public int? AcceptedByUserId { get; set; }
        public virtual User? AcceptedByUser { get; set; }

        public DateTimeOffset? DateTimeAccepted { get; set; }

        public string Code { get; set; } = null!;

        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}
