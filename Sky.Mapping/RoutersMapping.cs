using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sky.Mapping
{

    public class RoutersMapping : IEntityTypeConfiguration<RoutersEntity>
    {
        public void Configure(EntityTypeBuilder<RoutersEntity> builder)
        {

        }
    }
}
