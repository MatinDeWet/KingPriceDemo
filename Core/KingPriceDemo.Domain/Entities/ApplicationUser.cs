using Microsoft.AspNetCore.Identity;

namespace KingPriceDemo.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual User User { get; set; } = null!;
    }
}
