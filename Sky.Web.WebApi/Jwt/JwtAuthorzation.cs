using Sky.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sky.Entity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.DependencyInjection;

namespace Sky.Web.WebApi.Jwt
{
    public class JwtAuthorzation : IJwtAuthorization
    {
        IHttpContextAccessor _httpcontext;
        ICacheService _cacheService;
        public JwtAuthorzation(IHttpContextAccessor httpcontext, IServiceProvider cacheService)
        {
            _httpcontext = httpcontext;
            _cacheService = cacheService.GetService<MemoryCacheService>();
        }


        /// <summary>
        /// 创建Token值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回token 数据</returns>
        public dynamic CreateToken(UserEntity entity)
        {
            DateTime expirteTime= DateTime.UtcNow.AddMinutes(Convert.ToDouble(ConfigHelper.GetSectionValue("expiresAt")));
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("ID", entity.ID);
            payload.Add("UserName", entity.UserName);
            payload.Add("Email", entity.Email);
            payload.Add("RolesID", entity.RoleID);
            var tokenacces = new
            {
                UserId = entity.ID,
                //RolesID=entity.RoleID,
                entity.UserName,
                AccessToken = Encrypts.CreateToken(payload, Convert.ToInt32(ConfigHelper.GetSectionValue("expiresAt"))),
                Expires = new DateTimeOffset(expirteTime).ToUnixTimeSeconds(),
                Success=true
            };
            if (tokenacces.Success)
            {
                _cacheService.Add(entity.ID, tokenacces.AccessToken);
            }
            return tokenacces;
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public dynamic UpdateToken(string token)
        {
            JwtSecurityToken readtoken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            //加入黑名单
            if (!_cacheService.Exists(readtoken.Payload["ID"].ToString()) )
            {
                _cacheService.Add(readtoken.Payload["ID"].ToString(), token);
            }

            DateTime expirteTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(ConfigHelper.GetSectionValue("expiresAt")));
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("ID", readtoken.Payload["ID"]);
            payload.Add("UserName", readtoken.Payload["UserName"]);
            payload.Add("RolesID", readtoken.Payload["RolesID"]);
            payload.Add("Email", readtoken.Payload["Email"]);

            var tokenacces = new
            {
                UserId = readtoken.Payload["ID"],
                AccessToken = Encrypts.CreateToken(payload, Convert.ToInt32(ConfigHelper.GetSectionValue("expiresAt"))),
                Expires = new DateTimeOffset(expirteTime).ToUnixTimeSeconds(),
                Success = true
            };
            return tokenacces;
        }

        /// <summary>   
        /// 停用token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> ExpiryToken(string token)
        {
            var encodedJwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            
            throw new NotImplementedException();
        }


        /// <summary>
        /// 验证是否过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> IsExpire(string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前token 
        /// </summary>
        /// <returns></returns>
        public JwtSecurityToken GetCurrentToken()
        {
            var token = _httpcontext.HttpContext.Request.Headers["Authorization"];
            if (token.Count > 0)
            {
                JwtSecurityToken jtoken = new JwtSecurityTokenHandler().ReadJwtToken(token.ToString().Split(" ").Last());
                return jtoken;
            }
            return null;
        }


        /// <summary>
        /// 获取特定值
        /// </summary>
        /// <param name="field">约定: ID, UserName,RolesID</param>
        /// <returns></returns>
        public string GetField(string field)
        {
            var list = GetCurrentToken();
            if (list != null)
            {
                return list.Payload[field].ToString();
            }
            return null;
        }
    }
}
