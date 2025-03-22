namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById
{
    public record GetGroupByIdRequest(int Id) : IQuery<GetGroupByIdResponse>;
}
