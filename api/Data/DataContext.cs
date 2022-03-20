using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Cafe> Cafes { get; set; } = null!;
        public DbSet<Pet> Pets { get; set; } = null!;
        public DbSet<Slot> Slots { get; set; } = null!;
    }
}