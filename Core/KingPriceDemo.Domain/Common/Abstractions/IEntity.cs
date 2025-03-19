namespace KingPriceDemo.Domain.Common.Abstractions
{
    public interface IEntity<KeyType> : IEntity
    {
        public KeyType Id { get; set; }
    }

    public interface IEntity
    {
        public DateTimeOffset DateTimeCreated { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset? DateTimeDeleted { get; set; }
    }
}
