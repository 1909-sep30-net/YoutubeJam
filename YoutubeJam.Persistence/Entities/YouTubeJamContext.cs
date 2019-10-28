using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.Persistence.Entities
{
    /// <summary>
    /// Setting up DBContext for app
    /// </summary>
    public class YouTubeJamContext : DbContext
    {
        public DbSet<Creator> Creator { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Creator>()
                  .Property(b => b.PhoneNumber)
                  .IsRequired();
        }

        public YouTubeJamContext(DbContextOptions<YouTubeJamContext> options)
            : base(options)
        {
        }
    }
}
