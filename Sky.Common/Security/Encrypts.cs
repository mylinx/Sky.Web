using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
namespace Sky.Common
{
   public class Encrypts
    {
        /// <summary>
        /// 加密公共方法
        /// </summary>
        /// <param name="PasswordFormat"></param>
        /// <returns></returns>
        public static string EncryptPassword(string PasswordFormat)
        {
            return EncryptProvider.Md5(PasswordFormat, MD5Length.L32);
        }


        /// <summary>
        /// 创建jwtToken,采用微软内部方法，默认使用HS256加密，如果需要其他加密方式，请更改源码
        /// 返回的结果和CreateToken一样
        /// </summary>
        /// <param name="payLoad"></param>
        /// <param name="expiresMinute">有效分钟</param>
        /// <returns></returns>
        public static string CreateToken(Dictionary<string, object> payLoad, int expiresMinute)
        {

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>();

            //claims.Add(new Claim("random", new Random().Next(10000000,999999999).ToString()));
            //claims.Add(new Claim("issued", DateTime.UtcNow.ToString()));
            foreach (var key in payLoad.Keys)
            {
                 
                var tempClaim = new Claim(key,string.Format("{0}",payLoad[key]));
                claims.Add(tempClaim);
            }


            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(expiresMinute)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigHelper.GetSectionValue("JwtSecurityKey"))), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }



        /// <summary>
        /// 创建jwtToken,采用微软内部方法，默认使用HS256加密，如果需要其他加密方式，请更改源码
        /// 返回的结果和CreateToken一样
        /// </summary>
        /// <param name="payLoad"></param>
        /// <param name="expiresMinute">有效分钟</param>
        /// <returns></returns>
        public static ClaimsPrincipal GetClaimsPrinctpal(string  token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); // 创建一个JwtSecurityTokenHandler类，用来后续操作
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken; // 将字符串token解码成token对象
                if (jwtToken == null)
                    return null;
                var validationParameters = new TokenValidationParameters() // 生成验证token的参数
                {
                    RequireExpirationTime = true, // token是否包含有效期
                    ValidateIssuer = false, // 验证秘钥发行人，如果要验证在这里指定发行人字符串即可
                    ValidateAudience = false, // 验证秘钥的接受人，如果要验证在这里提供接收人字符串即可
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigHelper.GetSectionValue("JwtSecurityKey"))) // 生成token时的安全秘钥
                }; 
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                return principal; // 返回秘钥的主体对象，包含秘钥的所有相关信息
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
