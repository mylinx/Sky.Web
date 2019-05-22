using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;

namespace Sky.Mapping
{

public class MenuroleMapping: IEntityTypeConfiguration<MenuroleEntity>
{
   public void Configure(EntityTypeBuilder<MenuroleEntity> builder)
   {
		
   }
}
}