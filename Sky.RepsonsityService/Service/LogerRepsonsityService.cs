using Sky.Core;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sky.RepsonsityService.Service
{
    public class LogerRepsonsityService : ResponsitryBase<LogerEntity>, ILogerRepsonsityService
    {
        public LogerRepsonsityService() : base("MySqlConnection", DBType.MySql)
        {

        }
    }
}
