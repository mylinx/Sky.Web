using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sky.Web.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 中间件第二种写法
    /// </summary>
    public static class MMidelExtensions
    {
        public static IApplicationBuilder MMidel(this IApplicationBuilder app)
        {
            //DateTime dateTime=
            DataResult result = new DataResult()
            {
                Verifiaction = true,
                Message = "中间件的另一种写法"
            };
            RequestDelegate middleware(RequestDelegate next)
            {
                return context =>
                {
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                };
            }
            return app.Use(middleware);
        }
    }
}
