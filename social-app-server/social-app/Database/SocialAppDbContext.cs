using Microsoft.EntityFrameworkCore;
using social_app.Models;
using System.Reflection;

namespace social_app.Database
{
    public class SocialAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

        public SocialAppDbContext()
        {
            Database.EnsureCreated();
        }

        public SocialAppDbContext(DbContextOptions<SocialAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Post>()
                .HasOne(u => u.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(u => u.UserId);
        }
    }
}
