using DomainLayer;
using DomainLayer.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer
{
    public class AplicationAlbumConfig
    {
        public AplicationAlbumConfig(EntityTypeBuilder<Album> entityBuilder)
        {
            entityBuilder.Property(x => x.Name).HasMaxLength(50);
            entityBuilder.Property(x => x.ReleaseDate).IsRequired();
        }
    }
}
