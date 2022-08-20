using Microsoft.EntityFrameworkCore;
using social_app.Models;
using System.Reflection;

namespace social_app.Database
{
    public class SocialAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

        public SocialAppDbContext() {}

        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .IsRequired();
        }
    }
}
