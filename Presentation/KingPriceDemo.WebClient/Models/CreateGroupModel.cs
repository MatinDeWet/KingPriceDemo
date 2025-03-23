using System.ComponentModel.DataAnnotations;

namespace KingPriceDemo.WebClient.Models
{
    public class CreateGroupModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
