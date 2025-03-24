using KingPriceDemo.Domain.Common.Abstractions;
using MediatR;

namespace KingPriceDemo.Application.Common.CQRS
{
    public interface IPriorityNotificationHandler<in TNotification>
        : INotificationHandler<TNotification>
        where TNotification : IPriorityNotification
    {
        public int Priority { get; }
    }
}
