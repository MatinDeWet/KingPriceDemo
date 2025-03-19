using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KingPriceDemo.Persistence.Data.Context
{
    public class KingPriceContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public KingPriceContext() { }

        public KingPriceContext(DbContextOptions<KingPriceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(KingPriceContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
