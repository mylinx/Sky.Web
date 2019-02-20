using System;
using System.Collections.Generic;
using System.Text;

namespace Sky.Core
{
    public sealed abstract class ContextFactory 
    {
        public static IResponsitryBase<T> CreateDAL<T>(DBType pp) where T : class ,new()
        {
            IResponsitryBase<T> dal;
            switch (pp)
            {
                case (DBType.Sql):
                    dataAccess = new BaseDAL<T>();
                    break;
                case (DBType.Sql2005):
                    dataAccess = new SQL2005Dal<T>();
                    break;
                case (DBType.Oracle):
                    dataAccess = new OracleDal<T>();
                    break;
                case (DBType.MySql):
                    dataAccess = new MySqlDal<T>();
                    break;
                default:
                    dataAccess = new BaseDAL<T>();
                    break;
            }
        }
    }
}
