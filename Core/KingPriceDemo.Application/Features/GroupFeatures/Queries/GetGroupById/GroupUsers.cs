using KingPriceDemo.Application.Common.Extensions;
using KingPriceDemo.Domain.Enums;

namespace KingPriceDemo.Application.Features.GroupFeatures.Queries.GetGroupById
{
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
