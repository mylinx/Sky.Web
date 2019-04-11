using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Sky.Common;
using Sky.Web.WebApi.Jwt;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sky.Web.WebApi.ReturnViewModel;
using System.Net;

namespace Microsoft.AspNetCore.Mvc
{
    public class AuthorsFilters : IAsyncAuthorizationFilter
    {
        private readonly ICacheService _cacheService;
        private readonly IJwtAuthorization _jwtAuthorization;
        public AuthorsFilters(
            IServiceProvider cacheService,
            IJwtAuthorization jwtAuthorization
            )
        {
            this._cacheService = cacheService.GetService<MemoryCacheService>();
            _jwtAuthorization = jwtAuthorization;
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            DataResult result = new DataResult()
            {
                verifiaction = false,
            };

            try
            {
                var token = _jwtAuthorization.GetCurrentToken();
                if (token != null)
                {
                    if (!_cacheService.Exists(token.Payload["ID"].ToString()))
                    {
                        result.statecode = (int)HttpStatusCode.Unauthorized;
                        result.message = "token失效,请重新登录!";
                        return Task.FromResult(result);
                    }
                }
            }
            finally
            {
                    
            }
            return Task.CompletedTask;
        }
    }
}
