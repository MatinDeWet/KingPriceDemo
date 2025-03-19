using KingPriceDemo.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KingPriceDemo.Persistence.Data.Context
{
    public class KingPriceContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public KingPriceContext() { }

        public KingPriceContext(DbContextOptions<KingPriceContext> options) : base(options) { }
    }
}
