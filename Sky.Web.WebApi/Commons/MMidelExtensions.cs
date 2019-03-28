using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sky.Web.WebApi.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class MMidelExtensions
    {
        public static IApplicationBuilder MMidel(this IApplicationBuilder app)
        {
            //DateTime dateTime=
            DataResult result = new DataResult()
            {
                verifiaction = true,
                message = "中间件的另一种写法"
            };
            Func<RequestDelegate, RequestDelegate> middleware = next =>
            { 
                return context =>
                { 
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                };
            };
            return app.Use(middleware);
        }
    }
}
