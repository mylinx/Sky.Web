using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sky.Mapping
{
    public class LogerMapping : IEntityTypeConfiguration<LogerEntity>
    {
        public void Configure(EntityTypeBuilder<LogerEntity> builder)
        {

        }
    }
}
