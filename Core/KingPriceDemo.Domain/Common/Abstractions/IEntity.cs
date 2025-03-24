namespace KingPriceDemo.Domain.Common.Abstractions
{
    public interface IEntity<KeyType>
    {
        public KeyType Id { get; set; }
    }
}
