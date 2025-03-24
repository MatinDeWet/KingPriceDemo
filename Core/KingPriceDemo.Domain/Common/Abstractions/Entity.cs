namespace KingPriceDemo.Domain.Common.Abstractions
{
    public abstract class Entity<KeyType>
    {
        public KeyType Id { get; set; } = default!;
    }
}
