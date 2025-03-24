using KingPriceDemo.Domain.Common.Abstractions;

namespace KingPriceDemo.Domain.EventEntities
{
    public record DeleteUserEvent : IPriorityNotification;
}
