using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;

namespace Sky.Mapping
{

public class PerssionMapping: IEntityTypeConfiguration<PerssionEntity>
{
   public void Configure(EntityTypeBuilder<PerssionEntity> builder)
   {
		
   }
}
}