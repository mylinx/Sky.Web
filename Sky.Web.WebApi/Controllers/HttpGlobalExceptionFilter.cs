using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc.Filters;
using Sky.Common;

namespace Sky.Web.WebApi.Controllers
{
    /// <summary>
    /// 全局过滤
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Logger.Log.Error(context.Exception.Message.ToString());
        }
    }
}
