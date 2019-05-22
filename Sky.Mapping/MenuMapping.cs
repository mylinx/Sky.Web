using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;

namespace Sky.Mapping
{

public class MenuMapping: IEntityTypeConfiguration<MenuEntity>
{
   public void Configure(EntityTypeBuilder<MenuEntity> builder)
   {
		
   }
}
}