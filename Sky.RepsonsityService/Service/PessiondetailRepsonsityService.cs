using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sky.Core;
using Sky.Entity;
using System.Linq;
using Sky.RepsonsityService.IService;

namespace Sky.RepsonsityService.Service
{

    public class PessiondetailRepsonsityService : ResponsitryBase<PessiondetailEntity>, IPessiondetailRepsonsityService
    {
        public PessiondetailRepsonsityService() : base("MysqlConnetion", DBType.MySql)
        {

        }
    }
}
