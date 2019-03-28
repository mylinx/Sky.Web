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
using Sky.RepsonsityService.Service;

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
        /// 获取Token
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
                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    payload.Add("ID", entity.ID);
                    payload.Add("UserName", entity.UserName);
                    payload.Add("Email", entity.Email);

                    var tokenacces = new
                    {
                        AccessToken = Encrypts.CreateToken(payload, 30),
                        Expires = 3600
                    };
                    result.rows = tokenacces;
                    result.verifiaction = true;
                    result.message = "登陆成功!";
                }
                else
                {
                    result.message = "获取token令牌失败!";
                    result.verifiaction = true;
                }
            }
            catch (Exception ex)
            {
                result.message = "非法登陆!";
                return JObject.FromObject(result);
            }
            finally
            {

            }
            return JObject.FromObject(result);
        }

         
    }
}