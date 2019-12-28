using Microsoft.EntityFrameworkCore;

namespace PlayerSheetApp.Entity
{
    public class PlayerSheetContext : DbContext
    {
        public PlayerSheetContext()
        {

        }

        public PlayerSheetContext(DbContextOptions<PlayerSheetContext> options)
            : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
    }
}
