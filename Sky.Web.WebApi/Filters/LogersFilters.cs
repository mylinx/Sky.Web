using Microsoft.AspNetCore.Mvc.Filters;
using Sky.RepsonsityService.IService;
using Sky.Web.WebApi.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sky.Entity;
using System.Net;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 这是日志过滤器,全局日志错误都会执行到这里
    /// </summary>
    public class LogersFilters :  ExceptionFilterAttribute
    {
        ILogerRepsonsityService _logerRepsonsityService;
        public LogersFilters(ILogerRepsonsityService logerRepsonsityService)
        {
            _logerRepsonsityService = logerRepsonsityService;
        }


        public override void OnException(ExceptionContext context)
        {
            LogerEntity entity = new LogerEntity();

            entity.ID = Guid.NewGuid().ToString();
            entity.LogAction = context.ActionDescriptor.RouteValues["action"];
            entity.LogController = context.ActionDescriptor.RouteValues["controller"];
            entity.LogDate = DateTime.Now.ToString();
            entity.Loger = context.HttpContext.Request.Host.Value;
            entity.LogContents = context.Exception.Message.ToString();
            _logerRepsonsityService.InsertAsync(entity);
            _logerRepsonsityService.SaveChange();
            DataResult result = new DataResult()
            {
                verifiaction = false,
                statecode = (int)HttpStatusCode.ExpectationFailed,
                message= context.Exception.Message.ToString()
            };  
            context.Result = new BadRequestObjectResult(result); 
        }
    }
}
