using DomainLayer;
using DomainLayer.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer
{
    public class AplicationArtistConfig
    {
        public AplicationArtistConfig(EntityTypeBuilder<Artist> entityBuilder)
        {
            entityBuilder.Property(x => x.Name).HasMaxLength(100);
            entityBuilder.Property(x => x.Description).HasMaxLength(500);
            entityBuilder.Property(x => x.LogoUrl).HasMaxLength(100);
        }
    }
}
