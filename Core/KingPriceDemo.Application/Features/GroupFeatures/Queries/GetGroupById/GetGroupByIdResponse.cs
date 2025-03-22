using KingPriceDemo.Application.Common.Extensions;
using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById
{
    public record GetGroupByIdResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string InviteToken { get; set; } = null!;

        public List<GroupUsers> Users { get; set; } = new List<GroupUsers>();
    }

    public record GroupUsers
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public GroupRightsEnum Rights { get; set; }
        public string RightsText => Rights.GetDisplayName();

        public string? FullName { get; set; }

        public string? Surname { get; set; }
    }
}
