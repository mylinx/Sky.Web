using Sky.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sky.Entity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sky.Web.WebApi.Jwt
{
    public class JwtAuthorzation : IJwtAuthorization
    {
        IHttpContextAccessor _httpcontext;
        public JwtAuthorzation(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext; 
        }

        public dynamic CreateToken(UserEntity entity)
        {
            DateTime expirteTime= DateTime.UtcNow.AddMinutes(Convert.ToDouble(ConfigHelper.GetSectionValue("expiresAt")));
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("ID", entity.ID);
            payload.Add("UserName", entity.UserName);
            payload.Add("Email", entity.Email);

            var tokenacces = new
            {
                UserId = entity.ID,
                AccessToken = Encrypts.CreateToken(payload, Convert.ToInt32(ConfigHelper.GetSectionValue("expiresAt"))),
                Expires = new DateTimeOffset(expirteTime).ToUnixTimeSeconds(),
                Success=true
            };
            return tokenacces;
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<string> UpdateToken(string token)
        {
            
            throw new NotImplementedException();
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


        public string GetCurrentToken()
        {
            var token = _httpcontext.HttpContext.Request.Headers["Authorization"];

            return token.ToString();
        }
    }
}
