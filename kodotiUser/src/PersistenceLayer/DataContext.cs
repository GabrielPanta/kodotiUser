using DomainLayer;
using DomainLayer.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer
{
    public class DataContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.LazyLoadingEnabled = true;
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new AplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
            new AplicationArtistConfig(modelBuilder.Entity<Artist>());
            new AplicationAlbumConfig(modelBuilder.Entity<Album>());
            new AplicationSongConfig(modelBuilder.Entity<Song>());
        }
    }
}
