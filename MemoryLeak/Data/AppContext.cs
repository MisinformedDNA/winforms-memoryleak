using Microsoft.EntityFrameworkCore;

namespace MemoryLeak.Data
{
    public partial class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = default!;
    }
}
