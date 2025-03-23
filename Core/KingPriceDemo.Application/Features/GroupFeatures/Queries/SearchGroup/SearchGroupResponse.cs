using KingPriceDemo.Application.Common.Extensions;
using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.SearchGroup
{
    public class SearchGroupResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int UserCount { get; set; }

        public GroupRightsEnum GroupRights { get; set; }
        public string GroupRightsText => GroupRights.GetDisplayName();
    }
}
