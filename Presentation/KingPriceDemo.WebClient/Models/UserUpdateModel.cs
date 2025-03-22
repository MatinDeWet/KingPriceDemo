namespace KingPriceDemo.WebClient.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Surname { get; set; }

        public string Email { get; set; } = null!;

        public string? CellphoneNumber { get; set; }
    }
}
