using Microsoft.EntityFrameworkCore;

namespace RatesService
{
   public class RateDbContext : DbContext
    {
        public RateDbContext(DbContextOptions<RateDbContext> options) : base(options) { }
        public DbSet<Rate> Rates => Set<Rate>();
    }
}
