using DomainLayer;
using DomainLayer.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer
{
    public class AplicationSongConfig
    {
        public AplicationSongConfig(EntityTypeBuilder<Song> entityBuilder)
        {
            entityBuilder.Property(x => x.Name).HasMaxLength(50);
            entityBuilder.Property(x => x.DurationTime).IsRequired();
        }
    }
}
