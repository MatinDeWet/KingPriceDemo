namespace KingPriceDemo.Application.Features.UserFeatures.Queries.GetUserById
{
    public record GetUserByIdRequest(int Id) : IQuery<GetUserByIdResponse>;
}
