using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using Sky.Common;
using Sky.Web.WebApi.PostViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Sky.Web.WebApi.ReturnViewModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Sky.Web.WebApi.Controllers
{

    /// <summary>
    /// Jwt授权认证例子 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeLoginController : ControllerBase
    {
        IUserRepsonsityService _userRepsonsityService;
        /// <summary>
        /// 登陆验证
        /// </summary>
        public HomeLoginController(IUserRepsonsityService userRepsonsityService)
        {
            _userRepsonsityService = userRepsonsityService;
        }

        [Authorize]
        [Route("getvalue")]
        [HttpGet]
        public string GetValue()
        {
            var sss=   this.HttpContext.Request.Headers["Authorization"];
            JwtSecurityToken aaa= new JwtSecurityTokenHandler().ReadJwtToken(sss);

            return "value1";
        }


        /// <summary>
        /// 登陆接口
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public async Task<JObject> Login([FromBody]Post_UserViewModel obj)
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
                    //开始写入身份信息
                    var indenti = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    //var userPrincipal = new ClaimsPrincipal(indenti);

                    indenti.AddClaim(new Claim(ClaimTypes.Name, entity.UserName));
                    //indenti.AddClaim(new Claim("password", entity.pa));
                    indenti.AddClaim(new Claim(ClaimTypes.NameIdentifier, entity.ID));
                    indenti.AddClaim(new Claim("email", entity.Email));
                    //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(indenti));
                    await HttpContext.SignInAsync(indenti.AuthenticationType,
                                              new ClaimsPrincipal(indenti),
                                              new AuthenticationProperties
                                              {
                                                  IsPersistent = true,
                                                  RedirectUri = "/Home/Index",
                                                  ExpiresUtc = new System.DateTimeOffset(dateTime: DateTime.Now.AddMinutes(30)),
                                              });
                }

                result.verifiaction = true;
                result.message = "登陆成功!";
            }
            catch (Exception ex)
            {
                result.message = "非法登陆!";
                return JObject.FromObject(result);
            }
            finally
            {

            }
            return JObject.FromObject(result); ;
        }



        [Route("GetToken")]
        [HttpPost]
        public JObject Token([FromBody]Post_UserViewModel obj)
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
                    var claims = new Claim[]
                       {
                            new Claim(ClaimTypes.Name,entity.UserName),
                            new Claim(ClaimTypes.NameIdentifier,entity.ID.ToString()),
                       };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(ConfigHelper.GetSectionValue("JwtSecurityKey")));
                    var expires = DateTime.UtcNow.AddDays(28);//
                    var token = new JwtSecurityToken(
                                issuer: "issuer",
                                audience: "audience",
                                claims: claims,
                                notBefore: DateTime.Now,
                                expires: expires,
                                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                    //生成Token
                    string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    var tokenacces = new
                    {
                        AccessToken = jwtToken,
                        Expires = DateTime.UtcNow.AddDays(28)
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


        [Route("GetToken1")]
        [HttpPost]
        public JObject Token1([FromBody]Post_UserViewModel obj)
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


        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="_userEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public JObject Register([FromBody]UserEntity _userEntity)
        {
            DataResult result = new DataResult();
            result.verifiaction = false;
            try
            {
                Expression<Func<UserEntity, bool>> presseion = null;
                presseion = expres => expres.UserName == _userEntity.UserName;
                if (_userRepsonsityService.IsExists(presseion))
                {
                    result.message = "已存在该账号!";
                    return JObject.FromObject(result);
                }

                UserEntity userEntity = new UserEntity();
                userEntity.ID = Guid.NewGuid().ToString();
                userEntity.UserName = _userEntity.UserName;
                userEntity.PassWord = Encrypts.EncryptPassword(_userEntity.PassWord);

                userEntity.Email = _userEntity.Email;
                userEntity.IsLock = 0;
                userEntity.LoginLastDate = DateTime.Now.ToString();
                userEntity.CreateDate = DateTime.Now.ToString();
                userEntity.Remark = _userEntity.Remark;
                _userRepsonsityService.Insert(userEntity);

                result.verifiaction = true;
                result.message = "写入成功!";
            }
            catch (Exception ex)
            {
                result.message = ex.Message.ToString();
            }
            finally
            {

            }

            return JObject.FromObject(result);
        }
        
    }
}
