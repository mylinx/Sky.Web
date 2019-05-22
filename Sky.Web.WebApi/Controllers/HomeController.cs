using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sky.Common;
using Sky.Web.WebApi.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Sky.Web.WebApi.Models;
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

        [Route("Test")]
        [HttpGet]
        public JObject Test()
        {
            DataResult result = new DataResult()
            {
                Verifiaction = false,
                Message="这是测试",

            };
            
            _userRepsonsityService.Test();
            return JObject.FromObject(result);
        }


        /// <summary>
        /// 登陆获取Token
        /// </summary>
        /// <returns></returns>
        [Route("GetToken")] 
        [HttpPost]
        public JObject GetToken([FromBody]Post_UserViewModel obj)
        {
            DataResult result = new DataResult
            {
                Verifiaction = false
            };
            try
            {
                string name = obj.Name;
                string password = obj.Password;
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                {
                    result.Message = "账号或者密码不能为空!";
                    return JObject.FromObject(result);
                }

                var entity = _userRepsonsityService.Login(name, password);

                if (entity != null)
                {
                    result.Rows = _jwtAuthorization.CreateToken(entity);
                    result.Verifiaction = true;
                    result.Message = "登陆成功!";
                }
                else
                {
                    result.Message = "获取token令牌失败!";
                    result.Verifiaction = true;
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
            DataResult result = new DataResult
            {
                Verifiaction = false
            };
            try
            {
                if (string.IsNullOrEmpty(uid))
                {
                    result.Message = "用户ID不能为空!";
                }

                var jtoken = _jwtAuthorization.GetCurrentToken();
                if (string.Format("{0}", jtoken.Payload["ID"]) == uid)
                {
                    _cacheService.Remove(uid);
                    result.Verifiaction = true;
                    result.Message = "退出成功!";
                }
            }
            finally
            {

            }
            return JObject.FromObject(result);
        }

    }
}