using DomainLayer.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer
{
    public class AplicationUserConfig
    {
        public AplicationUserConfig(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.Property(x => x.FirstName).HasMaxLength(50);
            entityBuilder.Property(x => x.LastName).HasMaxLength(100);
        }
    }
}
