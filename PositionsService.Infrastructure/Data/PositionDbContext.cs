using Microsoft.EntityFrameworkCore;


namespace PositionsService
{
   public class PositionDbContext :DbContext
    {
        public PositionDbContext(DbContextOptions<PositionDbContext> options) : base(options) { }
        public DbSet<Position> Positions => Set<Position>();
    }
}
