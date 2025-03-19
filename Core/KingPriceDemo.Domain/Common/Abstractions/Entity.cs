namespace KingPriceDemo.Domain.Common.Abstractions
{
    public abstract class Entity<KeyType>
    {
        public KeyType Id { get; set; } = default!;

        public DateTimeOffset DateTimeCreated { get; set; } = DateTimeOffset.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset? DateTimeDeleted { get; set; }
    }
}
