namespace KingPriceDemo.Application.Features.UserFeatures.Queries.SearchUser
{
    public record SearchUserResponse
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Surname { get; set; }

        public string Email { get; set; } = null!;

        public string? CellphoneNumber { get; set; }
    }
}
