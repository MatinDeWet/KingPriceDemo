using MediatR;

namespace KingPriceDemo.Application.Common.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse>
            : IRequestHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
            where TResponse : notnull
    {
    }
}
