using KingPriceDemo.Domain.Common.Abstractions;
using MediatR;

namespace KingPriceDemo.Application.Common.CQRS.Publishers
{
    public class PriorityNotificationPublisher : INotificationPublisher
    {
        private const int default_priority = 99;

        public async Task Publish(
            IEnumerable<NotificationHandlerExecutor> handlerExecutors,
            INotification notification,
            CancellationToken cancellationToken
        )
        {
            var lookUp = handlerExecutors
                    .ToLookup(key => GetPriority(key.HandlerInstance), value => value)
                    .OrderBy(k => k.Key);

            foreach (var handler in lookUp)
            {
                foreach (var notificationHandler in handler.ToList())
                {
                    await notificationHandler.HandlerCallback(notification, cancellationToken);
                }
            }
        }

        private int GetPriority(object handler)
        {
            var priority = handler
                .GetType()
                .GetProperties()
                .FirstOrDefault(t =>
                    t.Name == nameof(IPriorityNotificationHandler<IPriorityNotification>.Priority)
                );

            if (priority == null)
                return default_priority;

            return int.Parse(priority.GetValue(handler)?.ToString() ?? default_priority.ToString());
        }
    }
}
