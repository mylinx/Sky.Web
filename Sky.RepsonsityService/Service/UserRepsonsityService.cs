using Sky.Core;
using System;
using Sky.Entity;
using System.Linq;
using Sky.RepsonsityService.IService; 
using Sky.Common; 
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore; 

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

        public void Test()
        {

            var str = base.GetAllList();
            var str1 = dbcontext.Set<UserEntity>();
            using (var tran = base.BeginTransaction())
            {
                
                try
                {
                    dbcontext.Database.ExecuteSqlCommand("Update userentity set username='3h1234555' where id='2343111232423'");


                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
          
           

            //UserEntity model = new UserEntity();

            //model = base.Find("2343111232423");


            // var aa1 = (from a in dbcontext.Set<UserEntity>()
            //            join b in dbcontext.Set<RolesEntity>() on a.RoleID == null ? "" : a.RoleID equals b.ID 
            //            into cc
            //            from c in cc.DefaultIfEmpty() //加入这句是left join, 去掉是执行inner join (right join 同理)
            //            select new
            //            {
            //                a.ID,
            //                c.RolesName,
            //                a.RoleID,
            //                a.Remark
            //            });



            //IQueryable aa= aa1.AsTracking();
            // string aa2 = aa.ToString();


            //aa1.Count();
            //var list = aa1.Skip(1).Take(20);

            //var aa = base.FromSql<dynamic>(" SELECT A.ID as aa , a.RoleID AS bb ,R.RolesName as cc FROM userentity A LEFT  JOIN ROLES  R on A.RoleID = R.ID ");
        }

        public class Models1
        {
            public string aa { get; set; }
            public string bb { get; set; }
            public string cc { get; set; }
        }
    }
}
