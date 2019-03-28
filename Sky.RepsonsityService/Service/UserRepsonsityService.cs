using Sky.Core;
using System;
using Sky.Entity;
using System.Linq;
using Sky.RepsonsityService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;
using Sky.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Sky.RepsonsityService.Service
{
    public class UserRepsonsityService : ResponsitryBase<UserEntity>, IUserRepsonsityService
    {
        public UserRepsonsityService() :base("MySqlConnection", DBType.MySql)
        {

        }

        /// <summary>
        /// 登陆方法
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserEntity Login(string  username, string password)
        {
            UserEntity model = new UserEntity();
            try
            {
                Expression<Func<UserEntity, bool>> pression = null;
                pression = pres => pres.UserName == username && pres.PassWord == Encrypts.EncryptPassword(password);
                model = this.GetFirstOrDefault(pression, null, null, false);
                if (model != null)
                { 
                    return model;
                }
            }
            finally
            {
                this.dbcontext.Dispose();
            }
            return null;
        }


        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public bool Register(UserEntity userEntity)
        {
            //判断其是否存在该ID
            try
            {
                Expression<Func<UserEntity, bool>> presseion = null;
                //if()
                presseion = expres => expres.UserName == userEntity.UserName.Trim();
                if (!base.IsExists(presseion))
                {
                    base.Insert(userEntity);
                }
            }
            finally
            {
                this.dbcontext.Dispose();
            }
            return false;
        }
    }
}
