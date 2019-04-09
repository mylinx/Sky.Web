using Sky.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.Jwt
{
    public interface IJwtAuthorization
    {

        /// <summary>
        /// 创建token值
        /// </summary>
        /// <returns></returns>
        dynamic CreateToken(UserEntity entity);

        /// <summary>
        /// 获取当前token
        /// </summary>
        /// <returns></returns>
        JwtSecurityToken GetCurrentToken();

        /// <summary>
        /// 刷新token值
        /// </summary>
        /// <returns></returns>
        dynamic UpdateToken(string token);

        /// <summary>
        /// 判断是否有效
        /// </summary>
        /// <returns></returns>
        Task<bool> IsExpire(string token);

        /// <summary>
        /// 停用Token
        /// </summary>
        /// <returns></returns>
        Task<bool> ExpiryToken(string token);


        /// <summary>
        /// 获取相对应的键值(约定有: ID ,)
        /// </summary>
        /// <param name="field">值约定: ID,UserName,Email,RolesID </param>
        /// <returns></returns>
        string GetField(string  field);
    }
}
