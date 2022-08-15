using Microsoft.EntityFrameworkCore;
using social_app.Models.User;
using System.Reflection;

namespace social_app.Database
{
    public class SocialAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public SocialAppDbContext() {}

        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
    }
}
