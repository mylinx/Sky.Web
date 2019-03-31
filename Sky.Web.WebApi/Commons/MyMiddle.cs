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

            //1.获取token 
            //2.通过token获取用户信息和角色信息
            //3.通过角色获取权限信息,进行路由匹对
            //为了提高效率, 可以用redis或者cache进行数据获取
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
