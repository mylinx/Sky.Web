using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.Entity;
namespace Sky.Mapping
{
    /// <summary>
    /// zlx 
    /// time: 2018-12-13 15:04:34
    /// 用于映射模型与表结构名称
    /// </summary>
    public class UserMapping : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            
        }
    }
}
