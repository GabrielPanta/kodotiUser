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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new AplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
        }
    }
}
