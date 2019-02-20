using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore; 
using System.Linq;
namespace Sky.Core
{
    public class HDBContext:DbContext
    {
        private string Connetionstring { get; }
        private DBType _dBType;
        public HDBContext(string conetionstring,DBType dBType)
        {
            Connetionstring = conetionstring;//链接字符串
            _dBType = dBType; //连接的数据库类型,如mysql或者sql
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
            base.OnConfiguring(optionsBuilder);
            if (!string.IsNullOrEmpty(Connetionstring))
            {
                switch (_dBType)
                {
                    case DBType.Sql:
                        optionsBuilder.UseSqlServer(Connetionstring, b => b.UseRowNumberForPaging());//配置Sql连接字符串
                        break;
                    case DBType.Access:
                        break;
                    case DBType.Oracle:
                        break; 
                    case DBType.MySql:
                        optionsBuilder.UseMySql(Connetionstring);//配置Mysql连接字符串
                        break;
                    default:
                        break;
                } 
            } 
         } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("Sky.Core.dll", "Sky.Mapping.dll").Replace("file:///", "");
            Assembly asm = Assembly.LoadFile(assembleFileName);
            var typesToRegister = asm.GetTypes()
                   .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
            .Where(type => type.GetTypeInfo().IsClass)
            .Where(type => type.GetTypeInfo().BaseType != null)
            .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null)
            .ToList();
            foreach (var entityType in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(entityType);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

        }
    }
}
