using System.ComponentModel.DataAnnotations;

namespace KingPriceDemo.WebClient.Models
{
    public class JoinGroupModel
    {
        [Required]
        public string InviteToken { get; set; } = null!;
    }
}
