using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sky.RepsonsityService.IService;
using Sky.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Sky.Web.WebApi.ViewModel;
using System.Reflection.Metadata;
using System.Data.SqlClient;
using System.Data;

namespace Sky.Web.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //INormalUserRepsonsityService _normalUser;
        //IUserRepsonsityService _userrespongy;
        //INormalRoleRepsonsityService _normalRoleRepsonsity;
        //INormalUserRoleRepsonsityService _normalUserRoleRepsonsityService;
        //public ValuesController(
        //    INormalUserRepsonsityService normalUser,
        //    IUserRepsonsityService userrespongy,
        //     INormalRoleRepsonsityService normalRoleRepsonsity,
        //     INormalUserRoleRepsonsityService normalUserRoleRepsonsityService)
        //{
        //    _normalUser = normalUser;
        //    _userrespongy = userrespongy;
        //    _normalRoleRepsonsity = normalRoleRepsonsity;
        //    _normalUserRoleRepsonsityService = normalUserRoleRepsonsityService;
        //}

        ///// <summary>
        ///// 测试多表链接查询
        ///// </summary>
        ///// <returns></returns>
        //[Route("Test1")]
        //[HttpGet]
        //public string Test1()
        //{
        //    Expression<Func<NormalUserRoleEntity, bool>> repdict = null;
        //    NormalUserEntity userid = _normalUser.GetById("279b3a01-1466-4676-a092-857fef886f28");

        //    if (userid != null)
        //    {
        //        repdict = prd => prd.NormalUserID == userid.ID;
        //        var userlist = from a in _normalUser.GetAllList().Take(10)
        //                       select new
        //                       {
        //                           UserName = a.Name,
        //                           Account = a.Account,
        //                           RoleName = GetName(a.ID) 
        //                       };
        //        return JsonConvert.SerializeObject(userlist);
        //    }
        //    return "123123";
        //}

        ///// <summary>
        ///// 无参数和有参数存储过程(完成)
        ///// </summary>
        ///// <returns></returns>
        //[Route("Test2")]
        //[HttpGet]
        //public string Test2()
        //{
        //    List<DBParameter> parameters = new List<DBParameter>();
        //    parameters.Add(new DBParameter("@name", "李琪玮"));
        //    string name = "李琪玮";

        //    var user = _normalUser.FromSql(@" exec GetUsers ", parameters);
        //    var users = _normalUser.FromSql($" exec GetUsers {name}");
        //    return "";
        //}


        ///// <summary>
        ///// 有参数并有返回值的存储过程
        ///// </summary>
        ///// <returns></returns>
        //[Route("Test3")]
        //[HttpGet]
        //public string Test3()
        //{
        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("name", "李琪玮"));

        //    parameters.Add(new SqlParameter("fuck", SqlDbType.VarChar,200));
        //    parameters[1].Direction = ParameterDirection.Output;

        //    var user111 = _normalUser.FromSql("exec GetUsersOutParam @name , @fuck out", parameters.ToArray());

        //    string sss = JsonConvert.SerializeObject(user111);

        //    return parameters[1].Value == null ? "" : parameters[1].Value.ToString();
        //}

        //private string GetName(string uid)
        //{
        //    Expression<Func<NormalUserRoleEntity, bool>> repdict = null;
        //    repdict = prd => prd.NormalUserID == uid;
        //    try
        //    {

        //        NormalRoleEntity normalRole = _normalRoleRepsonsity.GetById(_normalUserRoleRepsonsityService.GetFirstOrDefault(repdict, null, null, false).NormalRoleID);
        //        if (normalRole != null)
        //        {
        //            return normalRole.Name;
        //        }
        //        return "";
        //    }
        //    finally
        //    {

        //    }
        //}

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <returns>分页获取</returns>
        public string GetUsersList()
        { 
            return "";
        }

    }
}
