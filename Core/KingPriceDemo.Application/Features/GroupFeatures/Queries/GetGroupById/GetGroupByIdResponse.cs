using KingPriceDemo.Application.Common.Extensions;
using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById
{
    public record GetGroupByIdResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string InviteToken { get; set; } = null!;

        public GroupRightsEnum GroupRights { get; set; }
        public string GroupRightsText => GroupRights.GetDisplayName();

        public List<GroupUsers> Users { get; set; } = new List<GroupUsers>();
    }
}
