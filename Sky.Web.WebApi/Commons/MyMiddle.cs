using Microsoft.AspNetCore.Http;
using Sky.Web.WebApi.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sky.Common;
using Microsoft.Extensions.DependencyInjection;
using Sky.Web.WebApi.Jwt;
using Newtonsoft.Json.Linq;
using log4net;
using Sky.Common.Emuns;

namespace Sky.Web.WebApi.Commons
{

    /// <summary>
    /// 中间件拦截token失效
    /// </summary>
    public class MyMiddle
    {
        private readonly RequestDelegate _next;
        private readonly ICacheService _cacheService;
        private readonly IJwtAuthorization _jwtAuthorization;
        public MyMiddle(RequestDelegate next,
            IServiceProvider cacheService,
            IJwtAuthorization jwtAuthorization
            )
        {
            this._next = next;
            this._cacheService = cacheService.GetService<MemoryCacheService>();
            _jwtAuthorization = jwtAuthorization;
        }


        public Task Invoke(HttpContext context)
        {
            DataResult result = new DataResult()
            {
                verifiaction = false,
                message = "管道失效" + context.Request.Host.Value.ToString()
            };

            try
            {
                var token= _jwtAuthorization.GetCurrentToken();
                if (token != null)
                {
                    if (!_cacheService.Exists(token.Payload["ID"].ToString()))
                    {
                        result.statecode = (int)HttpStateEmuns.Invalid;
                        result.message = "token失效,请重新登录!";
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    }
                    else
                    {
                        result.verifiaction = true;
                    }
                }
                return _next(context);
            }
            catch (Exception ex)
            {
                result.statecode = (int)HttpStateEmuns.Danger;
                result.message = "非法请求!";
                return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
