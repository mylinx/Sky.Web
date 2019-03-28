using Microsoft.AspNetCore.Http;
using Sky.Web.WebApi.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sky.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Sky.Web.WebApi.Commons
{
    public class MyMiddle
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;
        public MyMiddle(RequestDelegate next, IServiceProvider cacheService)
        {
            this._next = next;
            this._cacheService = cacheService.GetService<RedisCacheService>();
        }


        public Task Invoke(HttpContext context)
        {
            DataResult result = new DataResult()
            {
                verifiaction = false,
                message = "管道失效" + context.Request.Host.Value.ToString()
            };
            long aa = 0;
            if (_cacheService.Exists(context.Request.Host.Value.ToString()))
            {
                string data = _cacheService.GetValue(context.Request.Host.Value.ToString());
                aa = DateTime.Parse(data).Ticks - DateTime.Now.Ticks;


                return context.Response.WriteAsync(aa+"aaaa"+ DateTime.UtcNow.ToString());
            }
            else
            {
                _cacheService.Add(context.Request.Host.Value.ToString(),DateTime.Now);
                 return  _next(context);
            } 
        }
    }
}
