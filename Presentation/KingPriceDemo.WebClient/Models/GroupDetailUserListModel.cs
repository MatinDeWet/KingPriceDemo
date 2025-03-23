namespace KingPriceDemo.WebClient.Models
{
    public class GroupDetailUserListModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public int Rights { get; set; }
        public string RightsText { get; set; } = null!;

        public string? FullName { get; set; }

        public string? Surname { get; set; }
    }
}
