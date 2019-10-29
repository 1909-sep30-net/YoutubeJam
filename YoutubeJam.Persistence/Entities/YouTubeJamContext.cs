using Microsoft.EntityFrameworkCore;

namespace YoutubeJam.Persistence.Entities
{
    /// <summary>
    /// Setting up DBContext for app
    /// </summary>
    public class YouTubeJamContext : DbContext
    {
        public DbSet<Creator> Creator { get; set; }
        public DbSet<Video> Video { get; set; }

        public DbSet<Analysis1> Analysis1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Creator>(entity =>
            {
                entity.Property(c => c.CID)
                .UseIdentityColumn();

                entity.Property(c => c.PhoneNumber)
                .IsRequired();

                entity.Property(c => c.Password)
                .IsRequired();

                entity.Property(c => c.FirstName)
                .IsRequired();

                entity.Property(c => c.LastName)
                .IsRequired();
            });
            modelBuilder.Entity<Video>(entity =>
               {
                   entity.Property(v => v.VID)
                   .UseIdentityColumn();

                   entity.Property(v => v.URL)
                   .IsRequired();
               }
                );
            modelBuilder.Entity<Analysis1>(entity =>
            {
                entity.Property(a => a.Anal1ID)
                .UseIdentityColumn();

                entity.Property(a => a.SentAve)
                .IsRequired();

                entity.Property(a => a.AnalDate)
                .IsRequired();
            }
            );
        }

        public YouTubeJamContext(DbContextOptions<YouTubeJamContext> options)
            : base(options)
        {
        }
    }
}