using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sky.Common;
using Sky.Web.WebApi.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Sky.Web.WebApi.ReturnViewModel;
using Sky.Web.WebApi.PostViewModel;
using Newtonsoft.Json.Linq;
using Sky.RepsonsityService.IService; 
using Microsoft.AspNetCore.Authorization; 

namespace Sky.Web.WebApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class HomeController : ControllerBase
    {
        IJwtAuthorization _jwtAuthorization;
        ICacheService _cacheService;
        IUserRepsonsityService _userRepsonsityService;
        public HomeController(IJwtAuthorization jwtAuthorization,
            IServiceProvider serviceProvider,
            IUserRepsonsityService userRepsonsityService
            )
        {
            this._jwtAuthorization = jwtAuthorization;
            this._cacheService = serviceProvider.GetService<MemoryCacheService>();
            this._userRepsonsityService = userRepsonsityService;
        }


        /// <summary>
        /// 登陆获取Token
        /// </summary>
        /// <returns></returns>
        [Route("GetToken")] 
        [HttpPost]
        public JObject GetToken([FromBody]Post_UserViewModel obj)
        {
            DataResult result = new DataResult();
            result.verifiaction = false;
            try
            {
                string name = obj.name;
                string password = obj.password;
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                {
                    result.message = "账号或者密码不能为空!";
                    return JObject.FromObject(result);
                }

                var entity = _userRepsonsityService.Login(name, password);

                if (entity != null)
                {
                    result.rows = _jwtAuthorization.CreateToken(entity);
                    result.verifiaction = true;
                    result.message = "登陆成功!";
                }
                else
                {
                    result.message = "获取token令牌失败!";
                    result.verifiaction = true;
                }
            }
            finally
            {

            }
            return JObject.FromObject(result);
        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("LoginOut")]
        [HttpPost]
        public JObject LoginOut(string uid)
        {
            DataResult result = new DataResult();
            result.verifiaction = false;
            try
            {
                if (string.IsNullOrEmpty(uid))
                {
                    result.message = "用户ID不能为空!";
                }

                var jtoken = _jwtAuthorization.GetCurrentToken();
                if (string.Format("{0}", jtoken.Payload["ID"]) == uid)
                {
                    _cacheService.Remove(uid);
                    result.verifiaction = true;
                    result.message = "退出成功!";
                }
            }
            finally
            {

            }
            return JObject.FromObject(result);
        }

    }
}