using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;

namespace Sky.Mapping
{

    public class RolesMapping : IEntityTypeConfiguration<RolesEntity>
    {
        public void Configure(EntityTypeBuilder<RolesEntity> builder)
        {
            
        }
    }
}
